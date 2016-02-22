using System;
using System.Threading;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Trukman
{
	public class DriverMainPage : ContentPage
	{
		public DriverMainPage ()
		{
			bool turnOnGPS = false;

			var locationService = DependencyService.Get<IGPSManager> ();
			if (locationService != null)
				turnOnGPS = locationService.IsTurnOnGPSLocation ();

			//var location = locationService.GetCurrentLocation ();
			//App.ServerManager.SaveDriverLocation (location);			

			var gpsAlert = new Label ();
			if (turnOnGPS)
				gpsAlert.Text = "GPS is turn on";
			else
				gpsAlert.Text = "You need to turn on GPS on your device!";

			var layout = new StackLayout () {
				Children={
					gpsAlert,
					new Label { Text = "Job list for " + App.ServerManager.GetCurrentUserName() },
				}	
			};

			TableView tableView = new TableView () {
				Intent = TableIntent.Data
			};
			TableSection section = new TableSection ();
			tableView.Root = new TableRoot () {
				section
			};
			section.Add (new ViewCell () { View = layout });
/*			section.Add (new TextCell (){ Text = "Job 1" });
			section.Add (new TextCell (){ Text = "Job 2" });*/

			//LoadJobList ();

			Content = tableView;

			LoadJobList();

			//tableView.

		}
	
		void LoadJobList ()
		{
			var tableView = (Content as TableView);
			//tableView.Invoke(
			var section = (TableSection)(tableView.Root[0]);

			var jobList = App.ServerManager.GetJobList ("");
			foreach(var job in jobList)
			{
				section.Add (new TextCell (){ Text = job.Name, Detail = job.Description });
				//section.Add (new TextCell (){ Text = "Job 2" });
			}
		}
	}
}
