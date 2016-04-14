using KAS.Trukman.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.Views.Pages
{
    #region ReceiverInfoPage
    public class ReceiverInfoPage : ContractorInfoPage
    {
        public ReceiverInfoPage() 
            : base()
        {
            this.BindingContext = new ReceiverInfoViewModel();
        }

        public new ReceiverInfoViewModel ViewModel
        {
            get { return (this.BindingContext as ReceiverInfoViewModel); }
        }
    }
    #endregion
}
