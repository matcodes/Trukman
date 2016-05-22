using KAS.Trukman.Classes;
using KAS.Trukman.Data.Classes;
using KAS.Trukman.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAS.Trukman.ViewModels.Pages.Owner
{
    #region OwnerImageViewerViewModel
    public class OwnerImageViewerViewModel : PageViewModel
    {
        public OwnerImageViewerViewModel() 
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

            this.Photo = (parameters != null && parameters.Length > 0 ? parameters[0] as Photo : null);
        }

        protected override void DoPropertyChanged(string propertyName)
        {
            if (propertyName == "Photo")
            {
                this.Title = (this.Photo != null ? this.Photo.Type : "");
                this.PhotoUri = (this.Photo != null ? this.Photo.Uri : null);
            }

            base.DoPropertyChanged(propertyName);
        }

        private void ShowPrevPage(object parameter)
        {
            PopPageMessage.Send();
        }

        public Photo Photo
        {
            get { return (this.GetValue("Photo") as Photo); }
            set { this.SetValue("Photo", value); }
        }

        public Uri PhotoUri
        {
            get { return (this.GetValue("PhotoUri") as Uri); }
            set { this.SetValue("PhotoUri", value); }
        }

        public VisualCommand ShowPrevPageCommand { get; set; }
    }
    #endregion
}
