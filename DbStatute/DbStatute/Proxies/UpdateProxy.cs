using DbStatute.Fundamentals.Proxies;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Querying.Builders;
using DbStatute.Querying.Builders;
using System;

namespace DbStatute.Querying
{
    public class UpdateProxy : StatuteProxyBase, IUpdateProxy
    {
        public UpdateProxy()
        {
        }

    }

    public class UpdateProxy<TModel> : StatuteProxyBase<TModel>, IUpdateProxy<TModel>
        where TModel : class, IModel, new()
    {
        public UpdateProxy()
        {
        }
    }
}