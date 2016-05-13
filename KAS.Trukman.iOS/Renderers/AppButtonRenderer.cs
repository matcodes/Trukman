using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using KAS.Trukman.Controls;
using KAS.Trukman.iOS;
using UIKit;
using CoreGraphics;

[assembly: ExportRenderer(typeof(ToolButton), typeof(ToolButtonRenderer))]
namespace KAS.Trukman.iOS
{
	#region AppButtonRenderer
	public class AppButtonRenderer : ButtonRenderer
	{
		protected override void OnElementChanged (ElementChangedEventArgs<Button> args)
		{
			base.OnElementChanged (args);

			var button = (this.Control as UIButton);
			if (button != null) {
				button.Layer.CornerRadius = 25.0f;
				button.Layer.BorderWidth = 2.0f;
				button.Layer.BorderColor = UIColor.Black.CGColor; // new CGColor(0xe6, 0xe6, 0xe6);
				button.Layer.SetNeedsDisplay();
//				button.ClipsToBounds = true;
			}
		}
	}
	#endregion
}

