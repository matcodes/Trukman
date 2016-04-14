using System;

using Xamarin.Forms;
using Trukman.ViewModels.Pages;

namespace Trukman
{
	#region ShipperInfoPage
	public class ShipperInfoPage : ContractorInfoPage
	{
		public ShipperInfoPage() : base()
		{
			this.BindingContext = new ShipperInfoViewModel();
		}

		public new ShipperInfoViewModel ViewModel
		{
			get { return (this.BindingContext as ShipperInfoViewModel); }
		}
	}
	#endregion
}
