﻿using KAS.Trukman.Messages;
using KAS.Trukman.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Trukman.Helpers;
using Trukman;

namespace KAS.Trukman
{
    #region App
    public class App : Application
	{
		static IServerManager serverManager;
		public static IServerManager ServerManager {
			get 
			{ 
				if (serverManager == null)
					serverManager = DependencyService.Get<IServerManager> ();
				return serverManager; 
			}
		}

		static ILocationService locManager;
		public static ILocationService LocManager {
			get 
			{ 
				if (locManager == null) 
					locManager = DependencyService.Get<ILocationService> ();
				return locManager; 
			}
		}


		static ILocationServicePlatformStarter locationServiceStarter;
		public static ILocationServicePlatformStarter LocationServiceStarter
		{
			get 
			{
				if (locationServiceStarter == null)
					locationServiceStarter = DependencyService.Get<ILocationServicePlatformStarter> ();
				return locationServiceStarter;
			}
		}

		public App ()
		{
			var _navigationPage = new NavigationPage ();
			if (!App.ServerManager.IsAuthorizedUser ())
				_navigationPage = new NavigationPage (new SignUpTypePage ());
			else
				this.MainPage = new MainPage ();;


			//this.MainPage = new MainPage ();
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
