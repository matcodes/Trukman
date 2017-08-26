using KAS.Trukman.Classes;
using KAS.Trukman.Data.Classes;
using KAS.Trukman.Languages;
using KAS.Trukman.Messages;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using KAS.Trukman.AppContext;
using KAS.Trukman.Extensions;

namespace KAS.Trukman.ViewModels.Pages
{
    #region PointsAndRewardsViewModel
    public class PointsAndRewardsViewModel : PageViewModel
    {
        public PointsAndRewardsViewModel() : base()
        {
            this.JobPointGroups = new ObservableCollection<JobPointGroup>();

            this.ShowMainMenuCommand = new VisualCommand(this.ShowMainMenu);
            this.ShowHomePageCommand = new VisualCommand(this.ShowHomePage);
            this.SelectJobPointCommand = new VisualCommand(this.SelectJobPoint);
            this.RefreshCommand = new VisualCommand(this.Refresh);
        }

        public override void Appering()
        {
            base.Appering();
        }

        public override void Disappering()
        {
            base.Disappering();
        }

        public override void Initialize(params object[] parameters)
        {
            base.Initialize(parameters);

            this.SelectJobPoints();
        }

        protected override void Localize()
        {
            base.Localize();

            this.Title = AppLanguages.CurrentLanguage.PointsAndRewardsPageName;
        }

        private void SelectJobPoints()
        {
            Task.Run(async () =>
            {
                this.IsBusy = true;
                try
                {
                    var jobPoints = await TrukmanContext.SelectJobPointsAsync();
                    this.ShowJobPoints(jobPoints);
                }
                catch (Exception exception)
                {
                    ShowToastMessage.Send(exception.Message);
                }
                finally
                {
                    this.IsRefreshing = false;
                    this.IsBusy = false;
                }
            }).LogExceptions("PointsAndRewardsViewModel SelectJobPoints");
        }

        private void ShowJobPoints(JobPoint[] jobPoints)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                this.JobPointGroups.Clear();
                this.SelectedJobPoint = null;

                foreach (var jobPoint in jobPoints)
                {
                    var group = this.JobPointGroups.FirstOrDefault(jpg => jpg.Job.ID == jobPoint.Job.ID);
                    if (group == null)
                    {
                        group = new JobPointGroup(jobPoint.Job);
                        this.JobPointGroups.Add(group);
                    }
                    group.Add(jobPoint);
                }
            });
        }

        private void ShowMainMenu(object parameter)
        {
            ShowMainMenuMessage.Send();
        }

        private void ShowHomePage(object parameter)
        {
            PopToRootPageMessage.Send();
        }

        private void SelectJobPoint(object parameter)
        {
            this.SelectedJobPoint = null;
        }

        private void Refresh(object parameter)
        {
            this.SelectJobPoints();
        }

        public ObservableCollection<JobPointGroup> JobPointGroups { get; private set; }

        public JobPoint SelectedJobPoint
        {
            get { return (this.GetValue("SelectedJobPoint") as JobPoint); }
            set { this.SetValue("SelectedJobPoint", value); }
        }

        public bool IsRefreshing
        {
            get { return (bool)this.GetValue("IsRefreshing", false); }
            set { this.SetValue("IsRefreshing", value); }
        }

        public VisualCommand SelectJobPointCommand { get; private set; }

        public VisualCommand ShowMainMenuCommand { get; private set; }

        public VisualCommand ShowHomePageCommand { get; private set; }

        public VisualCommand RefreshCommand { get; private set; }
    }
    #endregion

    #region JobPointGroup
    public class JobPointGroup : ObservableCollection<JobPoint>
    {
        private int _points = 0;

        public JobPointGroup(Trip job)
            : base()
        {
            this.Job = job;
            this.CollectionChanged += (sender, args) =>
            {
                if (args.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                    foreach (var item in args.NewItems)
                        this.Points += (item as JobPoint).Value;
                else if (args.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
                    foreach (var item in args.OldItems)
                        this.Points += (item as JobPoint).Value;
            };
        }

        public Trip Job { get; private set; }

        public int Points
        {
            get { return _points; }
            set
            {
                if (_points != value)
                {
                    _points = value;
                    this.OnPropertyChanged(new PropertyChangedEventArgs("Points"));
                }
            }
        }
    }
    #endregion
}
