using System;
//using Trukman.Languages;
using Xamarin.Forms;
using Trukman.Messages;
using KAS.Trukman.ViewModels.Pages;
using KAS.Trukman.Classes;
using KAS.Trukman.Languages;
using KAS.Trukman.Messages;
using KAS.Trukman.Data.Interfaces;

namespace Trukman.ViewModels.Pages
{
	public class SignUpCompanyViewModel : PageViewModel
	{
		public SignUpCompanyViewModel () : base()
		{
			this.PopPageCommand = new VisualCommand (this.PopPage);
		}

		public override void Appering ()
		{
			base.Appering ();
		}

		public override void Disappering ()
		{
			base.Disappering ();
		}

		protected override void DoPropertyChanged (string propertyName)
		{
			base.DoPropertyChanged (propertyName);
		}

		protected override void Localize ()
		{
			base.Localize ();

			this.Title = AppLanguages.CurrentLanguage.SignUpLabel;
		}

		protected override void DisableCommands()
		{
			base.DisableCommands();

			Device.BeginInvokeOnMainThread(() => 
				{
					this.PopPageCommand.IsEnabled = false;
				});
		}

		protected override void EnabledCommands()
		{
			base.EnabledCommands();

			Device.BeginInvokeOnMainThread(() => 
				{
					this.PopPageCommand.IsEnabled = true;
				});
		}

		private void PopPage(object parameter)
		{
			PopPageMessage.Send();
		}

        public VisualCommand PopPageCommand { get; private set; }
	}
}

