using KAS.Trukman.Classes;
using KAS.Trukman.Data.Interfaces;
using KAS.Trukman.Data.Maps;
using KAS.Trukman.Data.Route;
using KAS.Trukman.AppContext;
using KAS.Trukman.Helpers;
using KAS.Trukman.Languages;
using KAS.Trukman.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms.Maps;
using Xamarin.Forms;
using KAS.Trukman.Storage.ParseClasses;
using System.Linq;
using KAS.Trukman.Data.Classes;

namespace KAS.Trukman.ViewModels.Pages.Owner
{
	public class OwnerCurrentJobsViewModel:PageViewModel
	{
		public ObservableCollection<Photo> Photos { get; private set; }
		public ObservableCollection<Grouping<string, Photo>> PhotosGrouped { get; set; }

//		public ParseCompany company = null;

		public OwnerCurrentJobsViewModel (): base()
		{
			this.Photos = new ObservableCollection<Photo>();
			this.PhotosGrouped = new ObservableCollection<Grouping<string, Photo>>();

			this.RefreshCommand = new VisualCommand (this.Refresh);
			this.ShowMainMenuCommand = new VisualCommand(this.ShowMainMenu);
			this.ShowHomePageCommand = new VisualCommand(this.ShowHomePage);

		}

		public override void Initialize (params object[] parameters)
		{
			base.Initialize (parameters);
			this.FetchLatestUpdate ();
		}

		private void FetchLatestUpdate()
		{
			Task.Run(async () =>
				{
					this.IsBusy = true;
					try
					{
//						if (company == null) {
//							company = await TrukmanContext.FetchParseCompany(TrukmanContext.Company.Name);
//						}

//						if (company != null) {
							var photos = await TrukmanContext.SelectPhotosAsync(); //  GetPhotosFromCompany(company);
							this.ShowPhotoList(photos);
//						}
					}
					catch (Exception exception)
					{
						ShowToastMessage.Send(exception.Message);
					}
					finally
					{
						this.IsBusy = false;
						this.IsRefreshing = false;
					}
				});
		}

		private void ShowPhotoList(Photo[] photos)
		{
			Device.BeginInvokeOnMainThread (() => {
				this.Photos.Clear ();
				this.PhotosGrouped.Clear();

				if (photos != null) {
					foreach (var photo in photos) {
						this.Photos.Add (photo);
					}		

					var grouped = from photo in photos
						group photo by photo.Job.JobRef into photoGroup
						select new Grouping<string, Photo>(photoGroup.Key, photoGroup);

					foreach (var _group in grouped) {
						this.PhotosGrouped.Add(_group);
					}
				}
			});
		}

		private void Refresh(object parameter)
		{
			this.FetchLatestUpdate ();
		}

		private void ShowMainMenu(object parameter)
		{
			ShowMainMenuMessage.Send();
		}

		private void ShowHomePage(object parameter)
		{
			PopToRootPageMessage.Send();
		}


		public bool IsRefreshing
		{
			get { return (bool)this.GetValue ("IsRefreshing", false); }
			set { this.SetValue ("IsRefreshing", value); }
		}

		protected override void Localize()
		{
			base.Localize();

			this.Title = AppLanguages.CurrentLanguage.OwnerDeliveryUpdatePageName;
		}

		public VisualCommand RefreshCommand { get; private set; }
		public VisualCommand ShowMainMenuCommand { get; private set; }
		public VisualCommand ShowHomePageCommand { get; private set; }
	}

	public class Grouping<K, T> : ObservableCollection<T>
	{
		public K Key { get; private set; }

		public Grouping(K key, IEnumerable<T> items)
		{
			Key = key;
			foreach (var item in items)
				this.Items.Add(item);
		}
	}

}

