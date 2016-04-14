using KAS.Trukman.Classes;
using KAS.Trukman.Languages;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.ViewModels.Pages
{
    #region MainViewModel
    public class MainViewModel : PageViewModel
    {
        public MainViewModel()
            : base()
        {
            this.MainMenuViewModel = new MainMenuViewModel();
        }

        protected override void Localize()
        {
            base.Localize();

            this.Title = AppLanguages.CurrentLanguage.AppName;
        }

        public MainMenuViewModel MainMenuViewModel { get; private set; }
    }
    #endregion
}
