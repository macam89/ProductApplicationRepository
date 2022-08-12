
namespace ProductApplication.Logging
{
    public interface ILogging
    {
        void Information(string message);
        void Warning(string message);
        void Debug(string message);
        void Error(string message);
    }
}
