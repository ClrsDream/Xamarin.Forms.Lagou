using Lagou.UWP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;
using Windows.UI.Xaml;
using Xamarin.Forms;

[assembly: Dependency(typeof(GeolocatorService))]
namespace Lagou.UWP.Services {
    public class GeolocatorService : IGeolocatorService {
        public async Task<string> GetCityNameAsync() {
            var status = await Geolocator.RequestAccessAsync();
            switch (status) {
                case GeolocationAccessStatus.Allowed:
                    var city = await this.GetPosition();
                    return city;
                case GeolocationAccessStatus.Denied:
                    break;
                case GeolocationAccessStatus.Unspecified:
                    break;
            }

            return "";
        }

        private async Task<string> GetPosition() {
            var g = new Geolocator();
            var pos = await g.GetGeopositionAsync();
            if (pos != null) {
                var loc = await MapLocationFinder.FindLocationsAtAsync(pos.Coordinate.Point);
                if(loc != null && loc.Locations.Count > 0) {
                    return loc.Locations.First().Address.Town;
                }
            }
            return "";
        }
    }
}
