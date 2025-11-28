using RescueHub.Models.Entities;
using RescueHub.Models.Enums;

namespace RescueHub.Services
{
    public interface IRiskCalculatorService
    {
        RiskLevel Calculate(UserRequest request);
    }
}
