using System;
//using Trukman.Languages;
using Xamarin.Forms;
using Trukman.Messages;
using KAS.Trukman.ViewModels.Pages;
using KAS.Trukman.Classes;
using KAS.Trukman.Languages;
using KAS.Trukman.Messages;

namespace Trukman.ViewModels.Pages
{
	public class SignUpDriverViewModel : PageViewModel
	{
		public SignUpDriverViewModel () : base()
		{
			this.ShowPrevPageCommand = new VisualCommand (this.ShowPrevPage);
		}

		protected override void Localize ()
		{
			base.Localize ();

			this.Title = AppLanguages.CurrentLanguage.SignUpLabel.ToUpper ();
		}

		protected override void DisableCommands()
		{
			base.DisableCommands();

			Device.BeginInvokeOnMainThread(() => 
				{
					this.ShowPrevPageCommand.IsEnabled = false;
				});
		}

		protected override void EnabledCommands()
		{
			base.EnabledCommands();

			Device.BeginInvokeOnMainThread(() => 
				{
					this.ShowPrevPageCommand.IsEnabled = true;
				});
		}

		private void ShowPrevPage(object parameter)
		{
			PopPageMessage.Send();
		}

		public VisualCommand ShowPrevPageCommand { get; private set; }
	}
}

