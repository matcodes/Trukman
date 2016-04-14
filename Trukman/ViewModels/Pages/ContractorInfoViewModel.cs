using System;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Trukman.Data.Interfaces;
using System.Threading.Tasks;
using System.Linq;
using Trukman.Messages;

namespace Trukman.ViewModels.Pages
{
	#region ContractorInfoViewModel
	public class ContractorInfoViewModel : PageViewModel
	{
		public ContractorInfoViewModel() 
			: base()
		{
			this.ShowHomePageCommand = new VisualCommand(this.ShowHomePage);
			this.ShowPrevPageCommand = new VisualCommand(this.ShowPrevPage);
		}

		public override void Initialize(params object[] parameters)
		{
			base.Initialize(parameters);

			var contractor = (parameters != null && parameters.Length > 0 ? (parameters[0] as IContractor) : null);

			if (contractor != null) {
				this.Name = contractor.Name;
				this.Phone = contractor.Phone;
				this.Fax = contractor.Fax;
				this.AddressLineFirst = contractor.AddressLineFirst;
				this.AddressLineSecond = contractor.AddressLineSecond;

				this.FindAddress();
			}
		}

		private void FindAddress()
		{
			Task.Run(async () => {
				var geocoder = new Geocoder();
				var locations = await geocoder.GetPositionsForAddressAsync(this.AddressLineFirst + " " + this.AddressLineSecond);

				var location = locations.FirstOrDefault();
				if ((location.Latitude == 0) && (location.Longitude == 0))
				{
					locations = await geocoder.GetPositionsForAddressAsync(this.AddressLineSecond);
					location = locations.FirstOrDefault();
				}

				this.AddressPosition = location;
			});
		}

		public override void Appering()
		{
			base.Appering();
		}

		public override void Disappering()
		{
			base.Disappering();
		}

		private void ShowHomePage(object parameter)
		{
			PopToRootPageMessage.Send();
		}

		private void ShowPrevPage(object parameter)
		{
			PopPageMessage.Send();
		}

		public string Name
		{
			get { return (string)this.GetValue("Name"); }
			set { this.SetValue("Name", value); }
		}

		public string Phone
		{
			get { return (string)this.GetValue("Phone"); }
			set { this.SetValue("Phone", value); }
		}

		public string Fax
		{
			get { return (string)this.GetValue("Fax"); }
			set { this.SetValue("Fax", value); }
		}

		public string AddressLineFirst
		{
			get { return (string)this.GetValue("AddressLineFirst"); }
			set { this.SetValue("AddressLineFirst", value); }
		}

		public string AddressLineSecond
		{
			get { return (string)this.GetValue("AddressLineSecond"); }
			set { this.SetValue("AddressLineSecond", value); }
		}

		public Position AddressPosition
		{
			get { return (Position)this.GetValue("AddressPosition", new Position(0, 0)); }
			set { this.SetValue("AddressPosition", value); }
		}

		public VisualCommand ShowHomePageCommand { get; private set; }

		public VisualCommand ShowPrevPageCommand { get; private set; }
	}
	#endregion
}
