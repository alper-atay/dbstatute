using Basiclog;

namespace DbStatute
{
    public abstract class Statute
    {
        public IReadOnlyLogbook ReadOnlyLogs => Logs;
        protected ILogbook Logs { get; } = Logger.New();

        protected abstract void OnFailed();

        protected abstract void OnSucceed();
    }
}