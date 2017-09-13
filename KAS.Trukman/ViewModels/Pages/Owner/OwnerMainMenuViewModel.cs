using KAS.Trukman.Classes;
using KAS.Trukman.Languages;
using KAS.Trukman.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace KAS.Trukman.ViewModels.Pages.Owner
{
    #region OwnerMainMenuViewModel
    public class OwnerMainMenuViewModel : PageViewModel
    {
        public OwnerMainMenuViewModel()
            : base()
        {
            this.SelectItemCommand = new VisualCommand(this.SelectItem);

            this.ManageDriversMenuItem = new MenuItem(new VisualCommand(this.ManageDrivers));
            this.ManageDispatchersMenuItem = new MenuItem(new VisualCommand(this.ManageDispatchers));
            this.ManageFleetMenuItem = new MenuItem(new VisualCommand(this.ManageFleet));
            this.SettingsMenuItem = new MenuItem(new VisualCommand(this.Settings));
            this.HelpMenuItem = new MenuItem(new VisualCommand(this.Help));
            this.SelectLanguageMenuItem = new MenuItem(new VisualCommand(this.SelectLanguage));

            this.Items = new ObservableCollection<MenuItem>();
            this.Items.Add(this.ManageDriversMenuItem);
            this.Items.Add(this.ManageDispatchersMenuItem);
            this.Items.Add(this.ManageFleetMenuItem);
            this.Items.Add(this.SettingsMenuItem);
            this.Items.Add(this.HelpMenuItem);
            this.Items.Add(this.SelectLanguageMenuItem);

            this.Localize();
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

            if (this.ManageDriversMenuItem != null)
            {
                this.ManageDriversMenuItem.Label = AppLanguages.CurrentLanguage.OwnerMainMenuManageDriversLabel;
                this.ManageDispatchersMenuItem.Label = AppLanguages.CurrentLanguage.OwnerMainMenuManageDispatchersLabel;
                this.ManageFleetMenuItem.Label = AppLanguages.CurrentLanguage.OwnerMainMenuManageFleetLabel;
                this.SettingsMenuItem.Label = AppLanguages.CurrentLanguage.OwnerMainMenuSettingsLabel;
                this.HelpMenuItem.Label = AppLanguages.CurrentLanguage.OwnerMainMenuHelpLabel;
                this.SelectLanguageMenuItem.Label = AppLanguages.CurrentLanguage.OwnerMainMenuSelectLanguageLabel;
            }
        }

        private void SelectItem(object parameter)
        {
            this.SelectedItem = null;

            var menuItem = (parameter as MenuItem);
            if ((menuItem != null) && (menuItem.Command != null) && (menuItem.Command.CanExecute(parameter)))
            {
                HideMainMenuMessage.Send();
                menuItem.Command.Execute(parameter);
            }
        }

        private void ManageDrivers(object parameter)
        {
            PopToRootPageMessage.Send();
        }

        private void ManageDispatchers(object parameter)
        {
            PopToRootPageMessage.Send();
        }

        private void ManageFleet(object parameter)
        {
            PopToRootPageMessage.Send();
        }

        private void Settings(object parameter)
        {
            PopToRootPageMessage.Send();
        }

        private void Help(object parameter)
        {
            PopToRootPageMessage.Send();
        }

        private void SelectLanguage(object parameter)
        {
            PopToRootPageMessage.Send();
        }

        public MenuItem SelectedItem
        {
            get { return (this.GetValue("SelectedItem") as MenuItem); }
            set { this.SetValue("SelectedItem", value); }
        }

        public string SearchText
        {
            get { return (string)this.GetValue("SearchText"); }
            set { this.SetValue("SearchText", value); }
        }

        public VisualCommand SelectItemCommand { get; private set; }

        public ObservableCollection<MenuItem> Items { get; private set; }

        public MenuItem ManageDriversMenuItem { get; private set; }

        public MenuItem ManageDispatchersMenuItem { get; private set; }

        public MenuItem ManageFleetMenuItem { get; private set; }

        public MenuItem SettingsMenuItem { get; private set; }

        public MenuItem HelpMenuItem { get; private set; }

        public MenuItem SelectLanguageMenuItem { get; private set; }
    }
    #endregion
}
