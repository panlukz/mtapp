namespace Mtapp.Models
{
    public interface IActivityManager
    {
        ActivityPosition ActualPosition { get; set; }
        Activity CurrentActivity { get; set; }

        void StartActivity();
        void StopActivity();
    }
}