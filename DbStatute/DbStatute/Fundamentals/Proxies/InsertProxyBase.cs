using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals.Proxies;
using DbStatute.Interfaces.Fundamentals.Queries;
using DbStatute.Interfaces.Queries;
using DbStatute.Queries;
using System;

namespace DbStatute.Fundamentals.Proxies
{
    public abstract class InsertProxyBase : ProxyBase, IInsertProxyBase
    {
        protected InsertProxyBase()
        {
            FieldQuery = new FieldQuery();
        }

        protected InsertProxyBase(IFieldQuery fieldQuery)
        {
            FieldQuery = fieldQuery ?? throw new ArgumentNullException(nameof(fieldQuery));
        }

        public IFieldQuery FieldQuery { get; }
    }

    public abstract class InsertProxyBase<TModel> : ProxyBase<TModel>, IInsertProxyBase<TModel>
        where TModel : class, IModel, new()
    {
        protected InsertProxyBase()
        {
            FieldQuery = new FieldQuery<TModel>();
        }

        protected InsertProxyBase(IFieldQuery<TModel> fieldQuery)
        {
            FieldQuery = fieldQuery ?? throw new ArgumentNullException(nameof(fieldQuery));
        }

        public IFieldQuery<TModel> FieldQuery { get; }

        IFieldQuery IFieldableQuery.FieldQuery => FieldQuery;
    }
}