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
        protected UpdateProxyBase()
        {
            FieldQuery = new FieldQuery();
        }

        protected UpdateProxyBase(FieldQuery fieldQuery)
        {
            FieldQuery = fieldQuery ?? throw new ArgumentNullException(nameof(fieldQuery));
        }

        public IFieldQuery FieldQuery { get; }
    }

    public abstract class UpdateProxyBase<TModel> : ProxyBase<TModel>, IUpdateProxyBase<TModel>
        where TModel : class, IModel, new()
    {
        protected UpdateProxyBase()
        {
            FieldQuery = new FieldQuery<TModel>();
        }

        protected UpdateProxyBase(FieldQuery<TModel> fieldQuery)
        {
            FieldQuery = fieldQuery ?? throw new ArgumentNullException(nameof(fieldQuery));
        }

        public IFieldQuery<TModel> FieldQuery { get; }

        IFieldQuery IFieldableQuery.FieldQuery => throw new NotImplementedException();
    }
}