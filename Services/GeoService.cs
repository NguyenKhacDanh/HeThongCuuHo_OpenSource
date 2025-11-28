namespace RescueHub.Services
{
    public class GeoService : IGeoService
    {
        public double? DistanceKm(double? lat1, double? lon1, double? lat2, double? lon2)
        {
            if (!lat1.HasValue || !lon1.HasValue || !lat2.HasValue || !lon2.HasValue)
                return null;

            double R = 6371;
            double dLat = ToRad(lat2.Value - lat1.Value);
            double dLon = ToRad(lon2.Value - lon1.Value);
            double a =
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRad(lat1.Value)) * Math.Cos(ToRad(lat2.Value)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }

        private double ToRad(double x) => x * Math.PI / 180;
    }
}
