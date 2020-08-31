using DbStatute.Fundamentals.Proxies;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Querying.Builders;
using DbStatute.Querying.Builders;
using System;

namespace DbStatute.Querying
{
    public class MergeProxy : StatuteProxyBase, IMergeProxy
    {
    }

    public class MergeProxy<TModel> : StatuteProxyBase<TModel>, IMergeProxy<TModel>
        where TModel : class, IModel, new()
    {
    }
}