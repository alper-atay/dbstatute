using Basiclog;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying.Fundamentals;
using System;

namespace DbStatute.Querying
{
    public abstract class StatuteQueryBase : IStatuteQueryBase
    {
        public IReadOnlyLogbook ReadOnlyLogs => Logs;
        protected ILogbook Logs { get; } = Logger.NewLogbook();
    }

    public abstract class StatuteQueryBase<TModel> : IStatuteQueryBase<TModel>
        where TModel : class, IModel, new()
    {
        public Type ModelType => typeof(TModel);
    }
}