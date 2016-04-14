using System;
using Trukman.Messages;
using Trukman.Languages;

namespace Trukman.ViewModels.Pages
{
	public class DriverListViewModel : PageViewModel
	{
		public DriverListViewModel () : base()
		{
			this.ShowHomePageCommand = new VisualCommand(this.ShowHomePage);
			this.ShowPrevPageCommand = new VisualCommand(this.ShowPrevPage);
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

		protected override void DisableCommands ()
		{
			base.DisableCommands ();
		}

		protected override void Localize ()
		{
			base.Localize ();

			this.Title = AppLanguages.CurrentLanguage.DriverListLabel;
		}

		private void ShowHomePage(object parameter)
		{
			PopToRootPageMessage.Send();
		}

		private void ShowPrevPage(object parameter)
		{
			PopPageMessage.Send();
		}

		public VisualCommand ShowHomePageCommand { get; private set; }
		public VisualCommand ShowPrevPageCommand { get; private set; }
	}
}

