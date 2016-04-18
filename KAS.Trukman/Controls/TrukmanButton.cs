using System;
using Xamarin.Forms;

namespace Trukman
{
	public class TrukmanButton : Button {
		public int Tag;

		public TrukmanButton () {
			TextColor = Color.FromHex ("BEBEBE");
			BackgroundColor = Color.FromHex ("222222");
			BorderRadius = 22;
			//			 += (object sender, EventArgs e) => {
			//				BackgroundColor = Color.Gray;
			//			};
			HorizontalOptions = LayoutOptions.Fill;
		}
	}
}

