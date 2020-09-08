﻿using DbStatute.Interfaces.Queries;

namespace DbStatute.Interfaces.Fundamentals.Proxies
{
    public interface ISelectProxyBase : IProxyBase
    {
        ISearchQuery SearchQuery { get; }
    }

    public interface ISelectProxyBase<TModel> : IProxyBase<TModel>, ISelectProxyBase
        where TModel : class, IModel, new()
    {
        new ISearchQuery<TModel> SearchQuery { get; }
    }
}