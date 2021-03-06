using FlightPlanner.Core.DTO;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Services.Validators
{
    public class DepartureTimeValidator : IValidator
    {
        public bool IsValid(FlightRequest request)
        {
            return !string.IsNullOrEmpty(request?.DepartureTime);
        }
    }
}
