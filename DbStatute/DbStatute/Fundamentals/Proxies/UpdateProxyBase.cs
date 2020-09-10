using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals.Proxies;
using DbStatute.Interfaces.Fundamentals.Queries;
using DbStatute.Interfaces.Queries;
using DbStatute.Queries;
using System;

namespace DbStatute.Fundamentals.Proxies
{
    public abstract class UpdateProxyBase : ProxyBase, IUpdateProxyBase
    {
    }

    public abstract class UpdateProxyBase<TModel> : ProxyBase<TModel>, IUpdateProxyBase<TModel>
        where TModel : class, IModel, new()
    {
    }
}