using Basiclog;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying.Statutes;
using System;

namespace DbStatute.Querying.Statutes
{
    public abstract class StatuteQuery : IStatuteQuery
    {
        public IReadOnlyLogbook ReadOnlyLogs => Logs;
        protected ILogbook Logs { get; } = Logger.NewLogbook();
    }

    public abstract class StatuteQuery<TModel> : IStatuteQuery<TModel>
        where TModel : class, IModel, new()
    {
        public Type ModelType => typeof(TModel);
    }
}