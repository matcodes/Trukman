using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using KAS.Trukman.Controls;
using KAS.Trukman.iOS;
using UIKit;
using System.Drawing;
using CoreGraphics;

[assembly: ExportRenderer(typeof(AppEntry), typeof(AppEntryRenderer))]
namespace KAS.Trukman.iOS
{
    #region AppEntryRenderer
    public class AppEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (this.Control != null)
            {
                this.Control.BorderStyle = UITextBorderStyle.None;

                this.Control.Layer.BorderWidth = 1;
                this.Control.Layer.BorderColor = UIColor.White.CGColor;
                this.Control.Layer.CornerRadius = 25.0f;
                this.Control.BackgroundColor = UIColor.White;
                this.Control.LeftView = new UIView(new CGRect(0, 0, 25, 50));
                this.Control.LeftViewMode = UITextFieldViewMode.Always;
                this.Control.RightView = new UIView(new CGRect(0, 0, 25, 50));
                this.Control.RightViewMode = UITextFieldViewMode.Always;
                this.Control.VerticalAlignment = UIControlContentVerticalAlignment.Center;

                this.Control.AutocapitalizationType = UITextAutocapitalizationType.None;
                this.Control.ClipsToBounds = true;
            }
        }
    }
    #endregion
}

