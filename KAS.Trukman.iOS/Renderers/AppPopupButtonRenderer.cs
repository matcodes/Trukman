using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using KAS.Trukman.Controls;
using KAS.Trukman.iOS;
using UIKit;
using CoreGraphics;
using CoreAnimation;

[assembly: ExportRenderer(typeof(AppPopupButton), typeof(AppPopupButtonRenderer))]
namespace KAS.Trukman.iOS
{
    #region AppPopupButtonRenderer
    public class AppPopupButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (this.Control != null)
            {
                var button = (this.Control as UIButton);
                button.ContentEdgeInsets = new UIEdgeInsets(7, 4, 7, 4);
                button.Layer.BorderWidth = 1.0f;
                button.Layer.BorderColor = new CGColor(128, 128, 128);

                button.Layer.SetNeedsDisplay();
                button.ClipsToBounds = true;
            }
        }
    }
    #endregion
}

