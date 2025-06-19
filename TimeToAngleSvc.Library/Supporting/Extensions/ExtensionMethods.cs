using TimeToAngleSvc.Models;

namespace TimeToAngleSvc.Library.Supporting.Extensions
{
    public static class ExtensionMethods
    {
        public static Task<TimeToAngleResponse> Validate(this TimeToAngleRequest request)
        {
            TimeToAngleResponse response;

            if (!int.TryParse(request.Hour, out var hour) || !int.TryParse(request.Minute, out var minute))
            {
                response = new TimeToAngleResponse
                {
                    Angle = 0,
                    Success = false,
                    Message = "Invalid hour or minute format."
                };

                return Task.FromResult(response);
            }

            if (hour < 0 || hour > 23 || minute < 0 || minute > 59)
            {
                response = new TimeToAngleResponse
                {
                    Angle = 0,
                    Success = false,
                    Message = "Hour must be between 0-23 and minute must be between 0-59."
                };

                return Task.FromResult(response);
            }

            return Task.FromResult(new TimeToAngleResponse { Angle = 0, Message = string.Empty, Success = true });
        }
    }
}