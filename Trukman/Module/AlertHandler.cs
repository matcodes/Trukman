using System;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Trukman
{
	public class AlertHandler
	{
		public AlertHandler ()
		{
		}

		async static public Task<Boolean> ShowCheckDriver (String name) {
			return await App.Current.MainPage.DisplayAlert (null, String.Format ("Does Driver {0} Work for you", name), "YES", "NO");
		}
	}
}

