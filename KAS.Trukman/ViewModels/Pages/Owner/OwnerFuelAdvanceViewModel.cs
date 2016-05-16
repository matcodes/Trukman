using KAS.Trukman.AppContext;
using KAS.Trukman.Classes;
using KAS.Trukman.Data.Classes;
using KAS.Trukman.Languages;
using KAS.Trukman.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KAS.Trukman.ViewModels.Pages.Owner
{
    #region OwnerFuelAdvanceViewModel
    public class OwnerFuelAdvanceViewModel : PageViewModel
    {
        public OwnerFuelAdvanceViewModel()
            : base()
        {
            this.Advances = new ObservableCollection<Advance>();

            this.SelectAdvanceCommand = new VisualCommand(this.SelectAdvance);
            this.RefreshCommand = new VisualCommand(this.Refresh);
            this.ShowHomePageCommand = new VisualCommand(this.ShowHomePage);
            this.ShowMainMenuCommand = new VisualCommand(this.ShowMainMenu);
        }

        public override void Appering()
        {
            base.Appering();

            this.SelectAdvances();
        }

        public override void Disappering()
        {
            base.Disappering();
        }

        public override void Initialize(params object[] parameters)
        {
            base.Initialize(parameters);
        }

        protected override void Localize()
        {
            base.Localize();

            this.Title = AppLanguages.CurrentLanguage.OwnerFuelAdvancePageName; 
        }

        private void SelectAdvances()
        {
            Task.Run(async() => {
                this.IsBusy = true;
                try
                {
                    var advances = await TrukmanContext.SelectFuelAdvancesAsync();
                    this.ShowAdvances(advances);
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
            });
        }

        private void ShowAdvances(Advance[] advances)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                this.Advances.Clear();
                this.SelectedAdvance = null;

                foreach (Advance advance in advances)
                {
                    this.Advances.Add(advance);
                    if (this.SelectedAdvance == null)
                        this.SelectedAdvance = advance;
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

        private void SelectAdvance(object parameter)
        {

        }

        private void Refresh(object parameter)
        {
            this.SelectAdvances();
        }

        public ObservableCollection<Advance> Advances { get; private set; }

        public Advance SelectedAdvance
        {
            get { return (this.GetValue("SelectedAdvance") as Advance); }
            set { this.SetValue("SelectedAdvance", value); }
        }

        public bool IsRefreshing
        {
            get { return (bool)this.GetValue("IsRefreshing", false); }
            set { this.SetValue("IsRefreshing", value); }
        }

        public VisualCommand SelectAdvanceCommand { get; private set; }

        public VisualCommand ShowMainMenuCommand { get; private set; }

        public VisualCommand ShowHomePageCommand { get; private set; }

        public VisualCommand RefreshCommand { get; private set; }
    }
    #endregion
}
