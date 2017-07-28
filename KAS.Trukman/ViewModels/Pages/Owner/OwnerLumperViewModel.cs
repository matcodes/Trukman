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
    #region OwnerLumperViewModel
    public class OwnerLumperViewModel : PageViewModel
    {
        public OwnerLumperViewModel()
            : base()
        {
            this.Advances = new ObservableCollection<Advance>();

            this.SelectAdvanceCommand = new VisualCommand(this.SelectAdvance);
            this.RefreshCommand = new VisualCommand(this.Refresh);
            this.ShowHomePageCommand = new VisualCommand(this.ShowHomePage);
            this.ShowMainMenuCommand = new VisualCommand(this.ShowMainMenu);
            this.EditComcheckAcceptCommand = new VisualCommand(this.EditComcheckAccept);
            this.EditComcheckCancelCommand = new VisualCommand(this.EditComcheckCancel);
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

            this.Title = AppLanguages.CurrentLanguage.OwnerLumperPageName;
        }

        private void SelectAdvances()
        {
            Task.Run(async () => {
                this.IsBusy = true;
                try
                {
                    var advances = await TrukmanContext.SelectFuelAdvancesAsync(1);
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
                    this.Advances.Add(advance);
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

        private async void SelectAdvance(object parameter)
        {
            this.IsBusy = true;
            this.Comcheck = "";
            this.EditingAdvance = this.SelectedAdvance;
            this.SelectedAdvance = null;
            try
            {
                this.EditingAdvance.State = 2;
                this.EditingAdvance.Comcheck = "";
                await TrukmanContext.SetAdvanceStateAsync(this.EditingAdvance);
                this.EditComcheckPopupVisible = true;
            }
            catch (Exception exception)
            {
                ShowToastMessage.Send(exception.Message);
            }
            finally
            {
                this.IsBusy = false;
            }
        }

        private void Refresh(object parameter)
        {
            this.SelectAdvances();
        }

        private async void EditComcheckAccept(object parameter)
        {
            this.IsBusy = true;
            try
            {
                if (String.IsNullOrEmpty(this.Comcheck))
                    throw new Exception("Comcheck is empty.");

                this.EditingAdvance.State = 3;
                this.EditingAdvance.Comcheck = this.Comcheck;
                await TrukmanContext.SetAdvanceStateAsync(this.EditingAdvance);
                this.Advances.Remove(this.EditingAdvance);
                this.EditComcheckPopupVisible = false;
                this.EditingAdvance = null;
            }
            catch (Exception exception)
            {
                ShowToastMessage.Send(exception.Message);
            }
            finally
            {
                this.IsBusy = false;
            }
        }

        private void EditComcheckCancel(object parameter)
        {
            this.EditComcheckPopupVisible = false;
            this.EditingAdvance = null;
        }

        public ObservableCollection<Advance> Advances { get; private set; }

        public Advance SelectedAdvance
        {
            get { return (this.GetValue("SelectedAdvance") as Advance); }
            set { this.SetValue("SelectedAdvance", value); }
        }

        public Advance EditingAdvance
        {
            get { return (this.GetValue("EditingAdvance") as Advance); }
            set { this.SetValue("EditingAdvance", value); }
        }

        public bool IsRefreshing
        {
            get { return (bool)this.GetValue("IsRefreshing", false); }
            set { this.SetValue("IsRefreshing", value); }
        }

        public bool EditComcheckPopupVisible
        {
            get { return (bool)this.GetValue("EditComcheckPopupVisible", false); }
            set { this.SetValue("EditComcheckPopupVisible", value); }
        }

        public string Comcheck
        {
            get { return (string)this.GetValue("Comcheck"); }
            set { this.SetValue("Comcheck", value); }
        }

        public VisualCommand SelectAdvanceCommand { get; private set; }

        public VisualCommand ShowMainMenuCommand { get; private set; }

        public VisualCommand ShowHomePageCommand { get; private set; }

        public VisualCommand RefreshCommand { get; private set; }

        public VisualCommand EditComcheckCancelCommand { get; private set; }

        public VisualCommand EditComcheckAcceptCommand { get; private set; }
    }
    #endregion
}
