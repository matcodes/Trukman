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
    #region PaddingUITextField
    public class PaddingUITextField: UITextField
    {
        private UIEdgeInsets EdgeInsets { get; set; }

        public PaddingUITextField()
        {
            this.BorderStyle = UITextBorderStyle.None;

            //this.Layer.BorderWidth = 1;
            //this.Layer.BorderColor = UIColor.White.CGColor;
            this.Layer.CornerRadius = 25.0f;
            this.BackgroundColor = UIColor.White;
            this.AutocapitalizationType = UITextAutocapitalizationType.None;
            this.ClipsToBounds = true;

            EdgeInsets = new UIEdgeInsets(10, 25, 10, 25);
        }

        public override CGRect TextRect(CGRect forBounds)
        {
            return base.TextRect(EdgeInsets.InsetRect(forBounds));
        }

        public override CGRect EditingRect(CGRect forBounds)
        {
            return base.EditingRect(EdgeInsets.InsetRect(forBounds));
        }
    }
    #endregion

    #region AppEntryRenderer
    public class AppEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            if (this.Control == null)
            {
                SetNativeControl(new PaddingUITextField());
            }

            base.OnElementChanged(e);
        }
    }
    #endregion
}

