using System;
using Xamarin.Forms;

namespace Trukman
{
	public class TrukmanLabel: Label
	{
		public TrukmanLabel ()
		{
			TextColor = Color.FromHex (Constants.LabelFontColor);
		}
	}
}

