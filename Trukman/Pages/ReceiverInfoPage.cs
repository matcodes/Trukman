using System;

using Xamarin.Forms;
using Trukman.ViewModels.Pages;

namespace Trukman
{
	#region ReceiverInfoPage
	public class ReceiverInfoPage : ContractorInfoPage
	{
		public ReceiverInfoPage() 
			: base()
		{
			this.BindingContext = new ReceiverInfoViewModel();
		}

		public new ReceiverInfoViewModel ViewModel
		{
			get { return (this.BindingContext as ReceiverInfoViewModel); }
		}
	}
	#endregion
}
