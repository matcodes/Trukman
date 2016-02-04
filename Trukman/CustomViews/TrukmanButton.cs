using System;
using Xamarin.Forms;

namespace Trukman
{
	public class TrukmanButton : Button {
		public int Tag;

		public TrukmanButton () {
			TextColor = Color.White;
			BackgroundColor = Color.FromRgb (200, 200, 200);
			//			 += (object sender, EventArgs e) => {
			//				BackgroundColor = Color.Gray;
			//			};
			HorizontalOptions = LayoutOptions.Fill;
		}
	}
}

