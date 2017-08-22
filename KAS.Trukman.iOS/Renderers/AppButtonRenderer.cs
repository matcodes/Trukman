using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using KAS.Trukman.Controls;
using KAS.Trukman.iOS;
using UIKit;
using CoreGraphics;
using CoreAnimation;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(AppButton), typeof(AppButtonRenderer))]
namespace KAS.Trukman.iOS
{
    #region AppButtonRenderer
    public class AppButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> args)
        {
            base.OnElementChanged(args);

            var button = (this.Control as UIButton);
            if (button != null)
            {
                button.ContentEdgeInsets = new UIEdgeInsets(7, 4, 7, 4);
            }
        }
    }
	#endregion
}

