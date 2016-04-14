using System;
using Trukman.Languages;
using Xamarin.Forms;
using Trukman.Messages;

namespace Trukman.ViewModels.Pages
{
	public class SignUpOwnerViewModel : PageViewModel
	{
		public SignUpOwnerViewModel () : base()
		{
			this.ShowPrevPageCommand = new VisualCommand (this.ShowPrevPage);
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

