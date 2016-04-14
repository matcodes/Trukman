using System;
using Trukman.Messages;

namespace Trukman.ViewModels.Pages
{
	#region PageViewModel
	public class PageViewModel : BaseViewModel
	{
		public PageViewModel() : base("", "")
		{
			this.Localize();
		}

		public override void Appering()
		{
			base.Appering();

			LanguageSelectedMessage.Subscribe(this, this.LanguageSelected);
		}

		public override void Disappering()
		{
			base.Disappering();

			LanguageSelectedMessage.Unsubscribe(this);
		}

		private void LanguageSelected(LanguageSelectedMessage message)
		{
			this.Localize();
		}

		protected override void DoPropertyChanged(string propertyName)
		{
			base.DoPropertyChanged(propertyName);
		}

		protected virtual void Localize()
		{
		}

		protected virtual void DisableCommands()
		{
		}

		protected virtual void EnabledCommands()
		{
		}
	}
	#endregion
}
