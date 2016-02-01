using System;
using Xamarin.Forms;

namespace Trukman
{
	public class AlertHandler
	{
		public AlertHandler ()
		{
		}

		async static public void ShowCheckDriver (ContentPage page, String name) {
			Boolean answer = await page.DisplayAlert (null, String.Format("Does Driver {0} Work for you", name), "YES", "NO");
			if (answer) {

			}
		}
	}
}

