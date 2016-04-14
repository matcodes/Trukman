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
using System.ComponentModel;

[assembly: ExportRenderer(typeof(ToolButton), typeof(ToolButtonRenderer))]
namespace KAS.Trukman.Droid.Renderers
{
    #region ToolButtonRenderer
    public class ToolButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> args)
        {
            base.OnElementChanged(args);

            this.SetImage();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(sender, args);

            if (args.PropertyName == "ImageSourceName")
                this.SetImage();            
        }

        private void SetImage()
        {
            var button = (this.Control as Android.Widget.Button);

            var element = (this.Element as ToolButton);

            if ((button != null) && (element != null))
            {
                var resourceId = Forms.Context.Resources.GetIdentifier(element.ImageSourceName, "drawable", Forms.Context.PackageName);
                Android.Graphics.Drawables.Drawable image = null;
                if ((int)Android.OS.Build.VERSION.SdkInt >= 21)
                    image = Forms.Context.Resources.GetDrawable(resourceId, Forms.Context.Theme);
                else 
                    image = Forms.Context.Resources.GetDrawable(resourceId);
                button.SetBackground(image);
            }
        }
    }
    #endregion
}