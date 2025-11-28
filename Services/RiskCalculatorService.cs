using RescueHub.Models.Entities;
using RescueHub.Models.Enums;

namespace RescueHub.Services
{
    public class RiskCalculatorService : IRiskCalculatorService
    {
        public RiskLevel Calculate(UserRequest request)
        {
            int score = 0;
            score += 1;

            if (request.HasChildren == true) score += 3;
            if (request.HasElderly == true) score += 3;
            if (request.HasPregnantWoman == true) score += 4;
            if (request.HasDisabledPerson == true) score += 4;
            if (request.NeedMedicalSupport == true) score += 4;

            if (request.WaterLevel.HasValue)
            {
                if (request.WaterLevel.Value >= 150) score += 5;
                else if (request.WaterLevel.Value >= 80) score += 3;
                else if (request.WaterLevel.Value >= 30) score += 1;
            }

            if (request.IsNightTime) score += 3;

            if (request.PeopleCount.HasValue && request.PeopleCount.Value >= 5)
                score += 2;

            if (score >= 15) return RiskLevel.Critical;
            if (score >= 9) return RiskLevel.High;
            if (score >= 5) return RiskLevel.Medium;
            return RiskLevel.Low;
        }
    }
}
