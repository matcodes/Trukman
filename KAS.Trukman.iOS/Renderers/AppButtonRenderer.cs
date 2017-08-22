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
		protected override void OnElementChanged (ElementChangedEventArgs<Button> args)
		{
			base.OnElementChanged (args);

			var button = (this.Control as UIButton);
			if (button != null)
            {
                var appButton = (this.Element as AppButton);
                if (appButton.AppStyle == AppButtonStyle.Normal)
                {
                    button.ContentEdgeInsets = new UIEdgeInsets(7, 4, 7, 4);
                    button.Layer.CornerRadius = 25.0f;
                    button.Layer.BorderWidth = 2.0f;
                    button.Layer.BorderColor = UIColor.Black.CGColor; // new CGColor(0xe9, 0xe9, 0xe9);
                    button.Layer.BackgroundColor = UIColor.Black.CGColor;
                    button.SetTitleColor(UIColor.White, UIControlState.Normal);
                    button.SetTitleColor(UIColor.White, UIControlState.Focused);
                    //button.TintColor = UIColor.FromRGB(9, 9, 9);

                    button.Layer.SetNeedsDisplay();
                    button.ClipsToBounds = true;
                }
                //else if (appButton.AppStyle == AppButtonStyle.Left)
                //{
                //    button.ContentEdgeInsets = new UIEdgeInsets(7, 4, 7, 4);
                //    button.Layer.CornerRadius = 25.0f;
                //    button.Layer.BorderWidth = 2.0f;
                //    button.Layer.BorderColor = UIColor.Black.CGColor; // new CGColor(0xe9, 0xe9, 0xe9);
                //    button.Layer.BackgroundColor = UIColor.Black.CGColor;
                //    button.SetTitleColor(UIColor.White, UIControlState.Normal);
                //    button.SetTitleColor(UIColor.White, UIControlState.Focused);
                //    //button.TintColor = UIColor.FromRGB(9, 9, 9);

                //    button.Layer.SetNeedsDisplay();
                //    button.ClipsToBounds = true;
                //}
                //else if (appButton.AppStyle == AppButtonStyle.Right)
                //{

                //}
            }
        }
    }
	#endregion
}

