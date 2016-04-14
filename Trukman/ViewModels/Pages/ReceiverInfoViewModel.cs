using System;

using Xamarin.Forms;
using Trukman.Languages;

namespace Trukman.ViewModels.Pages
{
	#region ReceiverInfoViewModel
	public class ReceiverInfoViewModel : ContractorInfoViewModel
	{
		public ReceiverInfoViewModel() 
			: base()
		{
		}

		public override void Initialize(params object[] parameters)
		{
			base.Initialize(parameters);
		}

		public override void Appering()
		{
			base.Appering();
		}

		public override void Disappering()
		{
			base.Disappering();
		}

		protected override void Localize()
		{
			this.Title = AppLanguages.CurrentLanguage.ReceiverInfoPageName;
		}
	}
	#endregion
}
