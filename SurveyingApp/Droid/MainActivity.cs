using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

//using XLabs.Forms;
//using XLabs.Ioc;
//using XLabs.Platform.Services.Geolocation;
//using XLabs.Platform.Services;
//using XLabs.Platform.Device;
using Xamarin.Forms.Platform.Android;



namespace Surveys.Droid
{
	[Activity (Label = "Surveys.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
//	 public class MainActivity : XFormsApplicationDroid
	public class MainActivity : FormsApplicationActivity // not  XFormsApplicationDroid
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
//
//			var container = new SimpleContainer(); // Create a SimpleCOntainer
//			container.Register<IGeolocator, Geolocator>(); // Register the Geolocator
//			container.Register<IDevice> (t => AndroidDevice.CurrentDevice); // Register the Device
//			Resolver.SetResolver(container.GetResolver()); // Resolve it
//
//			global::Xamarin.Forms.Forms.Init (this, bundle);

			Xamarin.Forms.Forms.Init (this, bundle);
			LoadApplication (new App ());

		}
	}
}

