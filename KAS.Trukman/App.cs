using KAS.Trukman.Messages;
using KAS.Trukman.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KAS.Trukman
{
    #region App
    public class App : Application
	{
		public App ()
		{
            this.MainPage = new MainPage();
		}

        protected override void OnStart ()
		{
        }

		protected override void OnSleep ()
		{
        }

        protected override void OnResume ()
		{
		}
	}
    #endregion
}
