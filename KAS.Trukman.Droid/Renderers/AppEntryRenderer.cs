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
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using KAS.Trukman.Controls;
using KAS.Trukman.Droid.Renderers;

[assembly: ExportRenderer(typeof(AppEntry), typeof(AppEntryRenderer))]
namespace KAS.Trukman.Droid.Renderers
{
    #region AppEntryRenderer
    public class AppEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> args)
        {
            base.OnElementChanged(args);

            if (this.Control != null)
            {
                this.Control.SetBackgroundResource(Resource.Drawable.appentry);
                this.Control.SetTextColor(Forms.Context.Resources.GetColor(Resource.Color.appentry_textcolor));
            }
        }
    }
    #endregion
}