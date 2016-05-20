using KAS.Trukman.Data.Classes;
using KAS.Trukman.Data.Route;
using Newtonsoft.Json;
using Parse;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace KAS.Trukman.Helpers
{
    #region RouteHelper
    public static class RouteHelper
    {
        //        private static string GOOGLE_DIRECTION_URI = "https://maps.googleapis.com/maps/api/directions/json?origin={0}&destination={1}&key=AIzaSyBfw1myqNDTtcxotCKgGwTukxWqQeI6vAw";

        public static async Task<RouteResult> FindRouteForTrip(string origin, string destination)
        {
            RouteResult result = null;

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
                        // To do: Exception message
                        throw new Exception("Check internet connection");
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
                        // To do: Exception message
                        throw new Exception("Bad route response");
                    }
            }
            return result;
        }

        public static async Task<RouteResult> FindRouteForTrip(Trip trip)
        {
            var origin = "";
            var destination = "";
            if (trip != null)
            {
                if (trip.Shipper != null)
                    origin = trip.Shipper.Address;
                if (trip.Receiver != null)
                    destination = trip.Receiver.Address;
            }

            return await FindRouteForTrip(origin, destination);
        }

        /*
    [20:02:02]
    gneil90: четко)
[20:02:31]
    gneil90: смотри там error может возвращаться
[20:02:46] gneil90: с двумя сообщениями
[20:02:49] gneil90: ZERO_RESULTS
или
OVER_QUERY_LIMIT
[20:02:58] gneil90: первый если по адресу ничего не найдено
[20:03:04] gneil90: может toast показывать
[20:03:52] gneil90: "Address was not recognized. Ask dispatcher, if the address provided is valid"
[20:04:58]
    gneil90: второй, "Google Geocode query limit exceeded, please, report your dispatch"
    */

        public static async Task<Position> GetPositionByAddress(string address)
        {
            IDictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "address", address }
                };
            var latitude = 0.0d;
            var longitude = 0.0d;
            try
            {
                await ParseCloud.CallFunctionAsync<IDictionary<string, object>>("geocodeAddress", parameters).ContinueWith(t =>
                {
                    try
                    {
                        latitude = (double)(t.Result["lat"]);
                        longitude = (double)(t.Result["lng"]);
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                    }
                });
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            return new Position(latitude, longitude);
        }

        public static async Task<string> GetAddressByPosition(Position position)
        {
            IDictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "lat", position.Latitude },
                    { "lng", position.Longitude }
                };
            var address = "";
            try
            {
                await ParseCloud.CallFunctionAsync<IDictionary<string, object>>("reverseGeocodeAddress", parameters).ContinueWith(t =>
                {
                    try
                    {
                        address = (string)(t.Result["address"]);
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                    }
                });
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            return address;
        }

        public static double Distance(Position positionFrom, Position positionTo)
        {
            double theta = positionFrom.Longitude - positionTo.Longitude;
            double dist = Math.Sin(DegToRad(positionFrom.Latitude)) * Math.Sin(DegToRad(positionTo.Latitude)) +
                Math.Cos(DegToRad(positionFrom.Latitude)) * Math.Cos(DegToRad(positionTo.Latitude)) *
                Math.Cos(DegToRad(theta));
            dist = Math.Acos(dist);
            dist = RadToDeg(dist);
            dist = dist * 60 * 1.1515 * 1609.344;
            return dist;
        }

        private static double DegToRad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private static double RadToDeg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
    }
    #endregion
}
