using TimeToAngleSvc.Library.Interfaces;
using TimeToAngleSvc.Library.Supporting.Extensions;
using TimeToAngleSvc.Models;

namespace TimeToAngleSvc.Library.Implementations
{
    public class CalculateTimeAngleLibrary : ICalculateTimeAngleLibrary
    {
        public Task<TimeToAngleResponse> CalculateAngleAsync(TimeToAngleRequest request)
        {
            var validate = request.Validate();
            if (validate.Result.Success == false)
            {
                return Task.FromResult(new TimeToAngleResponse
                {
                    Angle = 0,
                    Success = false,
                    Message = validate.Result.Message
                });
            }

            try
            {
                var hours = int.Parse(request.Hour);
                var minutes = int.Parse(request.Minute);

                var hourInDegrees = hours * 30 + minutes * 30.0 / 60;
                var minuteInDegrees = minutes * 6;
                var angle = Math.Abs(hourInDegrees - minuteInDegrees);

                /*
                 * If the diff is greater than 180 degrees
                 * subtract the difference from 360 degrees
                 */

                if (angle > 180)
                {
                    angle = 360 - angle;
                }

                return Task.FromResult(new TimeToAngleResponse
                {
                    Angle = angle,
                    Success = true,
                    Message = "Angle calculated successfully."
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new TimeToAngleResponse
                {
                    Angle = 0,
                    Success = false,
                    Message = $"An error occurred while calculating the angle: {ex.Message}"
                });
            }
        }
    }
}