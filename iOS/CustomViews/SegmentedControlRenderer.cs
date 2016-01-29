using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using Trukman;

[assembly:ExportRenderer(typeof(SegmentedControl), typeof(Trukman.iOS.SegmentedControlRenderer))]
namespace Trukman.iOS
{
	public class SegmentedControlRenderer : ViewRenderer<SegmentedControl, UIKit.UISegmentedControl>
	{
		public SegmentedControlRenderer ()
		{
		}

		protected override void OnElementChanged (ElementChangedEventArgs<SegmentedControl> e)
		{
			base.OnElementChanged (e);

			var segmentedControl = new UIKit.UISegmentedControl ();

			for (var i = 0; i < e.NewElement.Children.Count; i++) {
				segmentedControl.InsertSegment (e.NewElement.Children [i].Text, i, false);
			}

			segmentedControl.ValueChanged += (sender, eventArgs) => {
				e.NewElement.SelectedValue = segmentedControl.TitleAt(segmentedControl.SelectedSegment);
			};

			SetNativeControl (segmentedControl);
		}
	}
}