using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using KAS.Trukman.Controls;
using KAS.Trukman.iOS;
using UIKit;

[assembly: ExportRenderer(typeof(ToolButton), typeof(ToolButtonRenderer))]
namespace KAS.Trukman.iOS
{
	#region ToolButtonRenderer
	public class ToolButtonRenderer : ButtonRenderer
	{
		protected override void OnElementChanged (ElementChangedEventArgs<Button> args)
		{
			base.OnElementChanged (args);

			this.SetImage ();
		}

		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs args)
		{
			base.OnElementPropertyChanged (sender, args);
	
			if (args.PropertyName == "ImageSourceName")
				this.SetImage();            
		}

		private void SetImage()
		{
			var element = (this.Element as ToolButton);
			var button = (this.Element as UIButton);

			if ((button != null) && (element != null) && (!String.IsNullOrEmpty (element.ImageSourceName))) {
				button.SetImage (UIImage.FromBundle (element.ImageSourceName), UIControlState.Normal);
			}
		}
	}
	#endregion
}

