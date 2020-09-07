using DbStatute.Fundamentals.Proxies;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Interfaces.Queries;
using DbStatute.Qualifiers;
using DbStatute.Queries;
using System;

namespace DbStatute.Proxies
{
    public class InsertProxy : InsertProxyBase, IInsertProxy
    {
        public InsertProxy()
        {
            ModelQuery = new ModelQuery();
            InsertedFieldQualifier = new FieldQualifier();
        }

        public InsertProxy(IModelQuery modelQuery, IFieldQualifier ınsertedFieldQualifier)
        {
            ModelQuery = modelQuery ?? throw new ArgumentNullException(nameof(modelQuery));
            InsertedFieldQualifier = ınsertedFieldQualifier ?? throw new ArgumentNullException(nameof(ınsertedFieldQualifier));
        }

        public IFieldQualifier InsertedFieldQualifier { get; }

        public IModelQuery ModelQuery { get; }
    }

    public class InsertProxy<TModel> : InsertProxyBase<TModel>, IInsertProxy<TModel>
        where TModel : class, IModel, new()
    {
        public InsertProxy()
        {
            ModelQuery = new ModelQuery<TModel>();
            InsertedFieldQualifier = new FieldQualifier<TModel>();
        }

        public InsertProxy(IModelQuery<TModel> modelQuery, IFieldQualifier<TModel> ınsertedFieldQualifier)
        {
            ModelQuery = modelQuery ?? throw new ArgumentNullException(nameof(modelQuery));
            InsertedFieldQualifier = ınsertedFieldQualifier ?? throw new ArgumentNullException(nameof(ınsertedFieldQualifier));
        }

        public IFieldQualifier<TModel> InsertedFieldQualifier { get; }

        IFieldQualifier IInsertProxy.InsertedFieldQualifier => InsertedFieldQualifier;

        public IModelQuery<TModel> ModelQuery { get; }

        IModelQuery IInsertProxy.ModelQuery => ModelQuery;
    }
}