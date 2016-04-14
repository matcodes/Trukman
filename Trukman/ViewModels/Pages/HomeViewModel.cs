using System;
using System.Windows.Input;
using Xamarin.Forms;
using Trukman.Messages;
using Trukman.Languages;

namespace Trukman.ViewModels.Pages
{
	public class HomeViewModel : PageViewModel
	{
		public HomeViewModel () : base()
		{
			this.ShowHomePageCommand = new VisualCommand(this.ShowHomePage);
			this.ShowPrevPageCommand = new VisualCommand(this.ShowPrevPage);
			//this.MessagesPageCommand = new VisualCommand ();
			this.MapPageCommand = new VisualCommand (this.ShowMapPage);
			//this.AdvancesPageCommand = new VisualCommand ();
			//this.ClockPageCommand = new VisualCommand ();
			//this.CameraPageCommand = new VisualCommand ();
		}

		public override void Appering ()
		{
			base.Appering ();

			// Если авторизовался водитель, то запускаем сервис отслеживания GPS-координат
			if (App.ServerManager.GetCurrentUserRole () == UserRole.UserRoleDriver)
				StartLocationServiceMessage.Send ();
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

		protected override void EnabledCommands ()
		{
			base.EnabledCommands ();
		}

		protected override void Localize ()
		{
			base.Localize ();

			this.Title = AppLanguages.CurrentLanguage.TrukmanLabel;
		}

		private void ShowHomePage(object parameter)
		{
			PopToRootPageMessage.Send();
		}

		private void ShowMapPage(object parameter)
		{
			ShowDriverListMessage.Send ();
		}

		private void ShowPrevPage(object parameter)
		{
			PopPageMessage.Send();
		}

		public VisualCommand ShowHomePageCommand { get; private set; }
		public VisualCommand ShowPrevPageCommand { get; private set; }
		public VisualCommand MessagesPageCommand { get; private set; }
		public VisualCommand MapPageCommand { get; private set; }
		public VisualCommand AdvancesPageCommand { get; private set; }
		public VisualCommand ClockPageCommand { get; private set; }
		public VisualCommand CameraPageCommand { get; private set; }
	}
}

