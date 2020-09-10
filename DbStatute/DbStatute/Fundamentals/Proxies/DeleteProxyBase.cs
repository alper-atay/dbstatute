using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals.Proxies;
using DbStatute.Interfaces.Fundamentals.Queries;
using DbStatute.Interfaces.Queries;
using DbStatute.Queries;
using RepoDb.Enumerations;
using System;

namespace DbStatute.Fundamentals.Proxies
{
    public abstract class DeleteProxyBase : ProxyBase, IDeleteProxyBase
    {
    }

    public abstract class DeleteProxyBase<TModel> : ProxyBase<TModel>, IDeleteProxyBase<TModel>
        where TModel : class, IModel, new()
    {
    }
}