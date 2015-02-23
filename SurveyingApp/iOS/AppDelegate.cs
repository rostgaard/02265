using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using XLabs.Forms;



namespace Surveys.iOS
{
	[Register ("AppDelegate")]
	// public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	public partial class AppDelegate : XFormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();

			LoadApplication (new App ());

			return base.FinishedLaunching (app, options);
         }
	}
}

