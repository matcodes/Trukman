using KAS.Trukman.Languages;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.ViewModels.Pages
{
    #region ShipperInfoViewModel
    public class ShipperInfoViewModel : ContractorInfoViewModel
    {
        public ShipperInfoViewModel() 
            : base()
        {
        }

        public override void Initialize(params object[] parameters)
        {
            base.Initialize(parameters);
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
            this.Title = AppLanguages.CurrentLanguage.ShipperInfoPageName;
        }
    }
    #endregion
}
