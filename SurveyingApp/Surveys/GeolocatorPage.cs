using System;

using Xamarin.Forms;
using XLabs.Platform.Device;
using XLabs.Platform;
using XLabs.Ioc;
using XLabs.Platform.Services.Geolocation;
using System.Threading.Tasks;
using System.Threading;
using Xamarin.Forms.Xaml;
using System.Diagnostics;

namespace Surveys
{
	public class GeolocatorPage : ContentPage
	{

		IGeolocator geolocator;
		CancellationTokenSource cancelSource;
		string PositionStatus;
		string PositionLatitude;
		string PositionLongitude;

		Label PositionStatusLabel;
		Label PositionLatitudeLabel;
		Label PositionLongitudeLabel;
		public GeolocatorPage ()
		{

			PositionStatusLabel = new Label{
				Text = "Position Satus"
			};

			PositionLatitudeLabel = new Label {
				Text = "Awaiting Latitude"
			};
				
			PositionLongitudeLabel = new Label {
				Text = "Awaiting Longitude"
			};

	

			Content = new StackLayout { 
				Children = {
					PositionStatusLabel,
					PositionLatitudeLabel,
					PositionLongitudeLabel
				}
			};

			Setup ();

		}

		void Setup() {
			var geolocator = Resolver.Resolve<IGeolocator> ();

			if (geolocator.IsGeolocationAvailable && geolocator.IsGeolocationEnabled)
				PositionStatusLabel.Text = "Geolocation available and enabled, getting location....";
			else 
				PositionStatusLabel.Text = "Please enable geolocation...";
			
			geolocator.PositionChanged += (object sender, PositionEventArgs e) => {
				PositionStatusLabel.Text = String.Format ("Got location at {0}", DateTime.Now.ToString ());
				PositionLatitudeLabel.Text = e.Position.Latitude.ToString ();
				PositionLongitudeLabel.Text = e.Position.Longitude.ToString ();
			};
			geolocator.StartListening (3000,0,true);
		}



		async Task GetPosition ()
		{
			Setup();

			this.cancelSource = new CancellationTokenSource();

			PositionStatus = String.Empty;
			PositionLatitude = String.Empty;
			PositionLongitude = String.Empty;
			IsBusy = true;

			await this.geolocator.GetPositionAsync (timeout: 10000, cancelToken: this.cancelSource.Token, includeHeading: true)
				.ContinueWith (t =>
					{
						IsBusy = false;
						if (t.IsFaulted)
							PositionStatus = ((GeolocationException)t.Exception.InnerException).Error.ToString();
						else if (t.IsCanceled)
							PositionStatus = "Canceled";
						else
						{
							PositionStatus = t.Result.Timestamp.ToString("G");
							PositionLatitude = "La: " + t.Result.Latitude.ToString("N4");
							PositionLongitude = "Lo: " + t.Result.Longitude.ToString("N4");
						}

					});
		}






	}
}


