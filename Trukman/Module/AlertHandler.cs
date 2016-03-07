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

		async static public Task<Boolean> ShowCheckDispatch (String name) {
			return await App.Current.MainPage.DisplayAlert (null, String.Format ("Does Dispatch {0} Work for you", name), "YES", "NO");
		}

		async static public Task ShowCheckRequestCompany(string companyName)
		{
			await App.Current.MainPage.DisplayAlert (null, string.Format ("Company {0} doesn't register in Trukman", companyName), "Ok");
		}

		async static public Task ShowAlert(string alert)
		{
			await App.Current.MainPage.DisplayAlert (null, alert, "Ok");
		}

		async static public Task<Boolean> ShowGpsAlert()
		{
			return await App.Current.MainPage.DisplayAlert(null, "TURN ON YOUR GPS", "Settings", "OK");
		}

		async static public Task ShowExceptionMessage(string login)
		{
			await App.Current.MainPage.DisplayAlert (null, string.Format("Incorrect password for login {0}", login), "OK");
		}
	}
}

