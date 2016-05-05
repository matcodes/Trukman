using KAS.Trukman.Languages;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.ViewModels.Pages.Owner
{
    #region OwnerMainViewModel
    public class OwnerMainViewModel : PageViewModel
    {
        public OwnerMainViewModel()
            : base()
        {
            this.MainMenuViewModel = new OwnerMainMenuViewModel();
        }

        protected override void Localize()
        {
            base.Localize();

            this.Title = AppLanguages.CurrentLanguage.AppName;
        }

        public OwnerMainMenuViewModel MainMenuViewModel { get; private set; }
    }
    #endregion
}
