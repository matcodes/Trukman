using KAS.Trukman.Data.Interfaces;
using KAS.Trukman.Data.Route;
using Newtonsoft.Json;
using Parse;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KAS.Trukman.Helpers
{
    #region RouteHelper
    public static class RouteHelper
    {
        private static string GOOGLE_DIRECTION_URI = "https://maps.googleapis.com/maps/api/directions/json?origin={0}&destination={1}&key=AIzaSyBfw1myqNDTtcxotCKgGwTukxWqQeI6vAw";

        public static async Task<RouteResult> FindRouteForTrip(ITrip trip)
        {
            RouteResult result = null;
            var origin = "";
            var destination = "";
            if (trip != null)
            {
                if (trip.Shipper != null)
                    origin = trip.Shipper.AddressLineFirst + " " + trip.Shipper.AddressLineSecond;
                if (trip.Shipper != null)
                    destination = trip.Receiver.AddressLineFirst + " " + trip.Receiver.AddressLineSecond;
            }

            if ((!String.IsNullOrEmpty(origin)) && (!String.IsNullOrEmpty(destination)))
            {
                var resultData = "";
                /*
                using (var client = new WebClient())
                {
                    try
                    {
                        var uri = String.Format(GOOGLE_DIRECTION_URI, origin, destination);
                        resultData = await client.DownloadStringTaskAsync(new Uri(uri));
                        Console.WriteLine(resultData);
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                        // To do: Show exception message
                    }
                }
                */

                var par = new Dictionary<string, object>();
                par.Add("origin", origin);
                par.Add("destination", destination);

                await ParseCloud.CallFunctionAsync<IDictionary<string, object>>("getMapRoute", par).ContinueWith(t => {
                    try
                    {
                        resultData = t.Result["text"].ToString();
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                        // To do: Show exception message
                    }
                });

                if (!String.IsNullOrEmpty(resultData))
                    try
                    {
                        result = JsonConvert.DeserializeObject<RouteResult>(resultData);
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                        // To do: Show exception message
                    }
            }
            return result;
        }
    }
    #endregion
}
