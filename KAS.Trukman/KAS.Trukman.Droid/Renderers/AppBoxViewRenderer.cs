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

using KAS.Trukman.Controls;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using KAS.Trukman.Droid.Renderers;

[assembly: ExportRenderer(typeof(AppBoxView), typeof(AppBoxViewRenderer))]
namespace KAS.Trukman.Droid.Renderers
{
    #region AppBoxViewRenderer
    public class AppBoxViewRenderer : ViewRenderer<AppBoxView, Android.Views.View>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<AppBoxView> args)
        {
            base.OnElementChanged(args);

            var view = new Android.Views.View(this.Context);
            view.SetBackgroundResource(Resource.Drawable.appboxview);
            SetNativeControl(view);
        }
    }
    #endregion
}