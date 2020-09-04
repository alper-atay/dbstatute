using Basiclog;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals.Proxies;
using System;

namespace DbStatute.Fundamentals.Proxies
{
    public abstract class ProxyBase : IProxyBase
    {
        public IReadOnlyLogbook ReadOnlyLogs => Logs;

        protected ILogbook Logs { get; } = Logger.NewLogbook();
    }

    public abstract class ProxyBase<TModel> : IProxyBase<TModel>
        where TModel : class, IModel, new()
    {
        public Type ModelType => typeof(TModel);
    }
}