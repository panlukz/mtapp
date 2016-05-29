using System.Threading.Tasks;

namespace Mtapp.Models
{
    public interface IActivityManager
    {
        ActivityPosition ActualPosition { get; set; }
        Activity CurrentActivity { get; set; }

        Task StartActivityAsync();
        Task StopActivityAsync();
    }
}