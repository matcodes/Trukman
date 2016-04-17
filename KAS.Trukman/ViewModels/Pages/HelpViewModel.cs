using KAS.Trukman.Classes;
using KAS.Trukman.Languages;
using KAS.Trukman.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.ViewModels.Pages
{
    #region HelpViewModel
    public class HelpViewModel : PageViewModel
    {
        public HelpViewModel()
            : base()
        {
            this.ShowMainMenuCommand = new VisualCommand(this.ShowMainMenu);
            this.ShowHomePageCommand = new VisualCommand(this.ShowHomePage);
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

            this.Title = AppLanguages.CurrentLanguage.HelpPageName;
        }

        private void ShowMainMenu(object parameter)
        {
            ShowMainMenuMessage.Send();
        }

        private void ShowHomePage(object parameter)
        {
            PopToRootPageMessage.Send();
        }

        public VisualCommand ShowMainMenuCommand { get; private set; }

        public VisualCommand ShowHomePageCommand { get; private set; }
    }
    #endregion
}
