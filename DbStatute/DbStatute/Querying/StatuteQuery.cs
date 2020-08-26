using Basiclog;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using System;

namespace DbStatute.Querying
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