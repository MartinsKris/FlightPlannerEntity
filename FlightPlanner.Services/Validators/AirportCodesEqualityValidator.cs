using FlightPlanner.Core.DTO;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Services.Validators
{
    public class AirportCodesEqualityValidator : IValidator
    {
        public bool IsValid(FlightRequest request)
        {
            return request?.To?.Airport?.Trim().ToLower() != request?.From?.Airport?.Trim().ToLower();
        }
    }
}
