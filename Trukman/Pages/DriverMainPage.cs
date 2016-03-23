using System;
using System.Threading;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Trukman
{
	public class DriverMainPage : BasePage
	{
		public DriverMainPage ()
		{
			var layout = new StackLayout () {
				Children={
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

			Content = tableView;

			LoadJobList();

			TryTurnOnGps ();
		}

		async void TryTurnOnGps ()
		{
			bool turnOnGPS = App.LocManager.IsTurnOnGPSLocation ();
			if (!turnOnGPS) {
				bool isTryTurnOn = await AlertHandler.ShowGpsAlert ();
				if (isTryTurnOn)
					App.LocManager.TryTurnOnGps ();
			}
		}
	
		async void LoadJobList ()
		{
			var tableView = (Content as TableView);
			var section = (TableSection)(tableView.Root[0]);
			var jobList = await App.ServerManager.GetJobList ("");
			foreach(var job in jobList)
			{
				Device.BeginInvokeOnMainThread( () =>
					{
						section.Add (new TextCell (){ Text = job.Name, Detail = job.Description });
						//section.Add (new TextCell (){ Text = "Job 2" });
					});
			}
		}
	}
}
