using System;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Trukman
{
	public class JobListPage : BasePage
	{
		TableView tableView;

		public JobListPage ()
		{
			NavigationPage.SetHasNavigationBar (this, false);

			Title = "Jobs";

			Button btnAddJob = new TrukmanButton ()
			{
				Text = "Add job",
			};
			btnAddJob.Clicked+= BtnAddJob_Clicked;

			//ListView listView = new ListView ();

			//listView.ItemsSource

			var layout = new StackLayout () {
				//VerticalOptions = LayoutOptions.Center,
				//Spacing = Constants.StackLayoutDefaultSpacing,
				//Padding = new Thickness (Constants.ViewsPadding),
				Children={
					btnAddJob,
					new Label { Text = "Job List" }
				}	
			};

			tableView = new TableView () {
				Intent = TableIntent.Data
			};

			TableSection section = new TableSection ();
			tableView.Root = new TableRoot () {
				section
			};

			section.Add (new ViewCell () { View = layout });

			Content = tableView;

			LoadJobList();
		}

		async void LoadJobList ()
		{
			var tableView = (Content as TableView);
			var section = (TableSection)(tableView.Root[0]);

			var view = section [0];
			section.Clear ();
			section.Add (view);
			string company = SettingsServiceHelper.GetCompany ();
			var jobList = await App.ServerManager.GetJobList (company);

			foreach(var job in jobList)
			{
				Device.BeginInvokeOnMainThread(() =>
					{
						section.Add (new TextCell (){ Text = job.Name, Detail = job.Description });
						//section.Add (new TextCell (){ Text = "Job 2" });
					});
			}
		}

		/*Grid GetTableGrid()
		{
			Grid grid = new Grid {
				VerticalOptions = LayoutOptions.FillAndExpand,
				//HeightRequest = 100,  //?????????
				//BackgroundColor = Color.Red,
				ColumnDefinitions = {
					new ColumnDefinition { Width = new GridLength (.3, GridUnitType.Star) },
//					new ColumnDefinition {Width = new GridLength(.3, GridUnitType.Star)},
//					new ColumnDefinition {Width = new GridLength(.3, GridUnitType.Star)},

				} 
			};

			//First Row
			grid.Children.Add(new Label {Text = row.Title, HorizontalOptions = LayoutOptions.Start}, 1, 0);
			grid.Children.Add(new Label {Text = "Market Value"}, 2, 0);
			grid.Children.Add(new Label {Text = row.MarketValue.ToString("C"), HorizontalOptions = LayoutOptions.End}, 3, 0);

			//Second Row
			grid.Children.Add(new Label {Text = " "}, 1, 1);
			grid.Children.Add(new Label {Text = "Return"}, 2, 1);
			grid.Children.Add(new Label {Text = row.ReturnRate.ToString("C"), HorizontalOptions = LayoutOptions.End}, 3, 1);
			return grid;
		}*/

		async void BtnAddJob_Clicked (object sender, EventArgs e)
		{
			//var jobPage = ;
			//jobPage.Disappearing += JobPage_Disappearing;
			await Navigation.PushModalAsync (new EditJobPage ());

			LoadJobList ();
			//task.Wait ();

			//var poppedPage = await Navigation.PopModalAsync ();
			//poppedPage.
			/*Job job = ((EditJob)jobPage).Job;
			if (job != null)
				App.ServerManager.SaveJob(job);*/
			//task.
			//task
			//tast.wa
		}
		/*
		async void JobPage_Disappearing (object sender, EventArgs e)
		{
			if (((EditJob)sender).Job != null)
				await App.ServerManager.SaveJob(((EditJob)sender).Job);
			else 
				await Task.FromResult(false);
		}*/
	}
}
