using TimeToAngleSvc.Models;

namespace TimeToAngleSvc.Library.Interfaces
{
    public interface ICalculateTimeAngleLibrary
    {
        public Task<TimeToAngleResponse> CalculateAngleAsync(TimeToAngleRequest request);
    }
}