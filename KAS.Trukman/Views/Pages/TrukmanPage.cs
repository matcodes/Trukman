using KAS.Trukman.Classes;
using KAS.Trukman.Helpers;
using KAS.Trukman.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KAS.Trukman.Views.Pages
{
    #region TrukmanPage
    public class TrukmanPage : ContentPage
    {
        public TrukmanPage() 
            : base()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            this.BackgroundImage = PlatformHelper.BackgroundImageSource;

            this.SizeChanged += (sender, args) => {
                if (this.Content == null) {

                    bool added = false;
                    int count = 0;

                    while ((!added) && (count < 3))
                    {
                        try
                        {
                            var content = this.CreateContent();
                            if (content != null)
                                this.Content = content;
                            added = true;
                        }
                        catch
                        {
                            count++;
                            if (count == 3)
                                ShowToastMessage.Send("Error create view.");
                        }
                    }
                }
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (this.ViewModel != null)
                this.ViewModel.Appering();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if (this.ViewModel != null)
                this.ViewModel.Disappering();
        }

        protected virtual View CreateContent()
        {
            return null;
        }

        public BaseViewModel ViewModel
        {
            get { return (this.BindingContext as BaseViewModel); }
        }
    }
    #endregion
}
