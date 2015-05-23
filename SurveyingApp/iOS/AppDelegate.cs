using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using XLabs.Forms;

using XLabs.Ioc;
using XLabs.Platform.Services.Geolocation;
using XLabs.Platform.Services;
using XLabs.Platform.Device;
using System.ComponentModel;



namespace Surveys.iOS
{
	[Register ("AppDelegate")]
	// public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	public partial class AppDelegate : XFormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{

			var container = new XLabs.Ioc.SimpleContainer ();
			container.Register<IDevice> (t => AppleDevice.CurrentDevice);
			container.Register<IGeolocator,Geolocator>();
			Resolver.SetResolver (container.GetResolver());
			global::Xamarin.Forms.Forms.Init ();

			LoadApplication (new App ());

			return base.FinishedLaunching (app, options);
         }
	}
}

