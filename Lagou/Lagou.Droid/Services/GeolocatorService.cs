using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Lagou.Droid.Services;
using Xamarin.Forms;
using Android.Locations;

[assembly: Dependency(typeof(GeolocatorService))]
namespace Lagou.Droid.Services {
    public class GeolocatorService : IGeolocatorService {

        //http://blog.csdn.net/litton_van/article/details/7101422
        //http://blog.csdn.net/i_lovefish/article/details/7948215

        public async Task<string> GetCityNameAsync() {
            var lm = LocationManager.FromContext(Forms.Context);

            Criteria criteria = new Criteria();
            criteria.Accuracy = Accuracy.Low;   //�߾���    
            criteria.AltitudeRequired = false;    //��Ҫ�󺣰�    
            criteria.BearingRequired = false; //��Ҫ��λ    
            criteria.CostAllowed = false; //�������л���    
            criteria.PowerRequirement = Power.Low;   //�͹���  

            var provider = lm.GetBestProvider(criteria, true);

            var loc = lm.GetLastKnownLocation(provider);//LocationManager.GpsProvider
            if (loc != null) {
                using (var g = new Geocoder(Forms.Context)) {
                    var addrs = await g.GetFromLocationAsync(loc.Latitude, loc.Longitude, 1);
                    if (addrs != null && addrs.Count > 0) {
                        var addr = addrs.First();
                        return addr.Locality;
                    }
                }
            }
            return "����";
        }
    }
}