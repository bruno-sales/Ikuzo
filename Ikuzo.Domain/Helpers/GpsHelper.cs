using System; 
using System.Device.Location; 
namespace Ikuzo.Domain.Helpers
{
    public class GpsHelper
    {

        public static int DistanceBetweenCoordenates(decimal firstLat, decimal firstLon, decimal secondLat,
            decimal secondLon)
        {
            var fLat = Convert.ToDouble(firstLat);
            var fLon = Convert.ToDouble(firstLon);
            var eLat = Convert.ToDouble(secondLat);
            var eLon = Convert.ToDouble(secondLon);

            var start = new GeoCoordinate(fLat, fLon);
            var end = new GeoCoordinate(eLat, eLon);

            var distance = start.GetDistanceTo(end);

            return Convert.ToInt32(distance);
        }

        public static string GetDirectionByAngle(int angle)
        {
            var direction = "West";

            if (angle > 315 && angle <= 45)
            {
                direction = "North";
            }
            else if (angle > 45 && angle <= 135)
            {
                direction = "East";
            }
            else if (angle > 135 && angle <= 225)
            {
                direction = "South";
            }

            return direction;
        }
    }
}
