using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies.Inserts;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Interfaces.Queries;
using DbStatute.Queries;
using System;

namespace DbStatute.Proxies.Inserts
{
    public class InsertProxyWithModelQuery : InsertProxy, IInsertProxyWithModelQuery
    {
        public InsertProxyWithModelQuery()
        {
            ModelQuery = new ModelQuery();
        }

        public InsertProxyWithModelQuery(IModelQuery modelQuery)
        {
            ModelQuery = modelQuery ?? throw new ArgumentNullException(nameof(modelQuery));
        }

        public InsertProxyWithModelQuery(IModelQuery modelQuery, IFieldQualifier insertedFieldQualifier) : base(insertedFieldQualifier)
        {
            ModelQuery = modelQuery ?? throw new ArgumentNullException(nameof(modelQuery));
        }

        public IModelQuery ModelQuery { get; }
    }

    public class InsertProxyWithModelQuery<TModel> : InsertProxy<TModel>, IInsertProxyWithModelQuery<TModel>
        where TModel : class, IModel, new()
    {
        public InsertProxyWithModelQuery()
        {
            ModelQuery = new ModelQuery<TModel>();
        }

        public InsertProxyWithModelQuery(IModelQuery<TModel> modelQuery)
        {
            ModelQuery = modelQuery ?? throw new ArgumentNullException(nameof(modelQuery));
        }

        public InsertProxyWithModelQuery(IModelQuery<TModel> modelQuery, IFieldQualifier<TModel> insertedFieldQualifier) : base(insertedFieldQualifier)
        {
            ModelQuery = modelQuery ?? throw new ArgumentNullException(nameof(modelQuery));
        }

        public IModelQuery<TModel> ModelQuery { get; }

        IModelQuery IInsertProxyWithModelQuery.ModelQuery => ModelQuery;
    }
}