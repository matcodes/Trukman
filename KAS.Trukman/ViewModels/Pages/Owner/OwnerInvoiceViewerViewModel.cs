using KAS.Trukman.Classes;
using KAS.Trukman.Languages;
using KAS.Trukman.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.ViewModels.Pages.Owner
{
    #region OwnerInvoiceViewerViewModel
    public class OwnerInvoiceViewerViewModel : PageViewModel
    {
        public OwnerInvoiceViewerViewModel() 
            : base()
        {
            this.ShowPrevPageCommand = new VisualCommand(this.ShowPrevPage);
        }

        public override void Appering()
        {
            base.Appering();
        }

        public override void Disappering()
        {
            base.Disappering();
        }

        public override void Initialize(params object[] parameters)
        {
            base.Initialize(parameters);

            this.InvoiceUri = (parameters != null && parameters.Length > 0 ? (string)parameters[0] : null);
        }

        protected override void Localize()
        {
            base.Localize();

            this.Title = AppLanguages.CurrentLanguage.OwnerInvoiceViewerPageName;
        }

        private void ShowPrevPage(object parameter)
        {
            PopPageMessage.Send();
        }

        public string InvoiceUri
        {
            get { return (string)this.GetValue("InvoiceUri"); }
            set { this.SetValue("InvoiceUri", value); }
        }

        public VisualCommand ShowPrevPageCommand { get; set; }
    }
    #endregion
}
