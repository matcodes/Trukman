using System;
using Xamarin.Forms;
using Trukman;
using Xamarin.Forms.Platform.Android;
using System.ComponentModel;
using Trukman.Droid.Renderers;

[assembly: ExportRenderer(typeof(ToolButton), typeof(ToolButtonRenderer))]
namespace Trukman.Droid.Renderers
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
				var image = Forms.Context.Resources.GetDrawable(resourceId, Forms.Context.Theme);
				button.SetBackground(image);
			}
		}
	}
	#endregion
}
