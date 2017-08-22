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
                this.Control.ContentEdgeInsets = new UIEdgeInsets(7, 4, 7, 4);

                this.Control.Layer.SetNeedsDisplay();
            }
        }
    }
    #endregion
}

