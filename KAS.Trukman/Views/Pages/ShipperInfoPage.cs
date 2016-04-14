using KAS.Trukman.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Views.Pages
{
    #region ShipperInfoPage
    public class ShipperInfoPage : ContractorInfoPage
    {
        public ShipperInfoPage() 
            : base()
        {
            this.BindingContext = new ShipperInfoViewModel();
        }

        public new ShipperInfoViewModel ViewModel
        {
            get { return (this.BindingContext as ShipperInfoViewModel); }
        }
    }
    #endregion
}
