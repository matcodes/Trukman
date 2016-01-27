using System;
using System.Collections.Generic;
using System.Linq;
using Parse;

using Foundation;
using UIKit;

namespace Trukman.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();

			App application = new App ();

			LoadApplication (application);

			App.ServerManager = new ServerManager();

			App.ServerManager.Init ();

			return base.FinishedLaunching (app, options);
		}
	}
}

