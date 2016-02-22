using System;
using Xamarin.Forms;

namespace Trukman
{
	public class RootPage : MasterDetailPage
	{
		RootMasterPage masterPage;

		public RootPage ()
		{
			UserRole currentUserRole = App.ServerManager.GetCurrentUserRole ();

			if (currentUserRole == UserRole.UserRoleDispatch) {
				masterPage = new RootMasterPage ();
				Master = masterPage;
				Detail = new JobListPage ();

				masterPage.ListView.ItemSelected += OnItemSelected;
			} 
			else if (currentUserRole == UserRole.UserRoleDriver) {
				masterPage = new RootMasterPage ();
				Master = masterPage;
				Detail = new DriverMainPage ();

				masterPage.ListView.ItemSelected += OnItemSelected;
			}
		}

		void OnItemSelected (object sender, SelectedItemChangedEventArgs e)
		{
			var item = e.SelectedItem as MasterPageItem;
			if (item != null) {
				Detail = new NavigationPage ((Page)Activator.CreateInstance (item.TargetType));
				masterPage.ListView.SelectedItem = null;
				IsPresented = false;
			}
		}
	}
}

