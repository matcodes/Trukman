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
using System.ComponentModel;
using Android.Graphics;

[assembly: ExportRenderer(typeof(AppButton), typeof(AppButtonRenderer))]
namespace KAS.Trukman.Droid.Renderers
{
    #region AppButtonRenderer
    public class AppButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> args)
        {
            base.OnElementChanged(args);

            if (this.Control != null)
            {
                var appButton = (this.Element as AppButton);
                var resource = Resource.Drawable.appbutton;
                if (appButton.AppStyle == AppButtonStyle.Left)
                    resource = Resource.Drawable.appbutton_left;
                else if (appButton.AppStyle == AppButtonStyle.Right)
                    resource = Resource.Drawable.appbutton_right;

                this.Control.SetBackgroundResource(resource);
                this.Control.SetTextColor(Forms.Context.Resources.GetColor(Resource.Color.appbutton_textcolor));
                this.Control.SetAllCaps(false);
            }
        }
    }
    #endregion
}