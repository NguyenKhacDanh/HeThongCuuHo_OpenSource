namespace RescueHub.Services
{
    public interface IGeoService
    {
        double? DistanceKm(double? lat1, double? lon1, double? lat2, double? lon2);
    }
}
