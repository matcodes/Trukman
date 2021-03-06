﻿using System;

using Xamarin.Forms;

namespace Trukman
{
	#region TrukmanPage
	public class TrukmanPage : ContentPage
	{
		public TrukmanPage() 
			: base()
		{
			NavigationPage.SetHasNavigationBar(this, false);

			this.BackgroundImage = "background";

			this.SizeChanged += (sender, args) => {
				if (this.Content == null) {
					var content = this.CreateContent();
					if (content != null)
						this.Content = content;
				}
			};
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (this.ViewModel != null)
				this.ViewModel.Appering();
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			if (this.ViewModel != null)
				this.ViewModel.Disappering();
		}

		protected virtual View CreateContent()
		{
			return null;
		}

		public BaseViewModel ViewModel
		{
			get { return (this.BindingContext as BaseViewModel); }
		}
	}
	#endregion
}
