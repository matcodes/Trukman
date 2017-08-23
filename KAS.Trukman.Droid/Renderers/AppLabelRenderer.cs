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

[assembly: ExportRenderer(typeof(AppLabel), typeof(AppLabelRenderer))]
namespace KAS.Trukman.Droid.Renderers
{
    #region AppLabelRenderer
    public class AppLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> args)
        {
            base.OnElementChanged(args);

            //if (this.Control != null)
            //{
            //    var element = (this.Element as AppLabel);
            //    var resource = Resource.Drawable.applabel;
            //    resource = Resource.Drawable.applabel;
            //    this.Control.SetBackgroundResource(resource);
            //}
        }
    }
    #endregion
}