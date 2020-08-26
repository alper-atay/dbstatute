using Basiclog;
using DbStatute.Interfaces.Querying.Statutes;

namespace DbStatute.Querying.Statutes
{
    public abstract class StatuteQuery : IStatuteQuery
    {
        public IReadOnlyLogbook ReadOnlyLogs => Logs;
        protected ILogbook Logs { get; } = Logger.NewLogbook();
    }
}