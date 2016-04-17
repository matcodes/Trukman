using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using KAS.Trukman.Controls;
using KAS.Trukman.Droid.Renderers;

[assembly: ExportRenderer(typeof(AppPopupButton), typeof(AppPopupButtonRenderer))]
namespace KAS.Trukman.Droid.Renderers
{
    #region AppPopupButtonRenderer
    public class AppPopupButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> args)
        {
            base.OnElementChanged(args);

            if (this.Control != null)
            {
                var appButton = (this.Element as AppButton);
				var resource = Resource.Drawable.apppopupbutton_left;
                if (appButton.AppStyle == AppButtonStyle.Right)
                    resource = Resource.Drawable.apppopupbutton_right;

                this.Control.SetBackgroundResource(resource);
				this.Control.SetTextColor(Forms.Context.Resources.GetColor(Resource.Color.apppopupbutton_textcolor));
            }
        }
    }
    #endregion
}