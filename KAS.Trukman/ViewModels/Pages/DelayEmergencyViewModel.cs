using KAS.Trukman.Classes;
using KAS.Trukman.Languages;
using KAS.Trukman.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using KAS.Trukman.AppContext;
using KAS.Trukman.Data.Classes;

namespace KAS.Trukman.ViewModels.Pages
{
    #region DelayEmergencyViewModel
    public class DelayEmergencyViewModel : PageViewModel
    {
        public DelayEmergencyViewModel() 
            : base()
        {
            this.ShowHomePageCommand = new VisualCommand(this.ShowHomePage);
            this.ShowPrevPageCommand = new VisualCommand(this.ShowPrevPage);
            this.SelectItemCommand = new VisualCommand(this.SelectItem);
            this.SubmitCommand = new VisualCommand(this.Submit);
        }

        public override void Initialize(params object[] parameters)
        {
            base.Initialize(parameters);

			var trip = (parameters != null && parameters.Length > 0 ? (parameters[0] as Trip) : null);
			this.Trip = trip;
        }

        public override void Appering()
        {
            base.Appering();
        }

        public override void Disappering()
        {
            base.Disappering();
        }

        protected override void Localize()
        {
            base.Localize();

            this.Title = AppLanguages.CurrentLanguage.DelayEmergencyPageName;
        }

        protected override void DisableCommands()
        {
            Device.BeginInvokeOnMainThread(() => {
                this.ShowHomePageCommand.IsEnabled = false;
                this.ShowPrevPageCommand.IsEnabled = false;
                this.SelectItemCommand.IsEnabled = false;
                this.SubmitCommand.IsEnabled = false;
            });

            base.DisableCommands();
        }

        protected override void EnabledCommands()
        {
            Device.BeginInvokeOnMainThread(() => {
                this.ShowHomePageCommand.IsEnabled = true;
                this.ShowPrevPageCommand.IsEnabled = true;
                this.SelectItemCommand.IsEnabled = true;
                this.SubmitCommand.IsEnabled = true;
            });

            base.EnabledCommands();
        }

        private void ShowHomePage(object parameter)
        {
            ShowMainMenuMessage.Send();
        }

        private void ShowPrevPage(object parameter)
        {
            PopToRootPageMessage.Send();
        }

        private void SelectItem(object parameter)
        {
            if (parameter != null)
                this.SelectedItem = (DelayEmergencyItems)parameter;
        }

        private void Submit(object parameter)
        {
            Task.Run(async () => {
                this.DisableCommands();
                this.IsBusy = true;
                try
                {
					await TrukmanContext.SendJobAlertAsync(this.Trip.ID, (int)this.SelectedItem, this.Comments);

                    this.ShowPrevPage(null);
                }
                catch (Exception exception)
                {
                    ShowToastMessage.Send(exception.Message);
                }
                finally
                {
                    this.IsBusy = false;
                    this.EnabledCommands();
                }
            });
        }

		public Trip Trip { get; private set; }

        public DelayEmergencyItems SelectedItem
        {
            get { return (DelayEmergencyItems)this.GetValue("SelectedItem", DelayEmergencyItems.FlatTire); }
            set { this.SetValue("SelectedItem", value); }
        }

        public string Comments
        {
            get { return (string)this.GetValue("Comments"); }
            set { this.SetValue("Comments", value); }
        }

        public VisualCommand ShowHomePageCommand { get; private set; }

        public VisualCommand ShowPrevPageCommand { get; private set; }

        public VisualCommand SelectItemCommand { get; private set; }

        public VisualCommand SubmitCommand { get; private set; }
    }
    #endregion

    #region DelayEmergencyItems
    public enum DelayEmergencyItems
    {
        FlatTire = 0,
        FeelingSleepy = 1,
        RoadWorkAhead = 2
    }
    #endregion
}
