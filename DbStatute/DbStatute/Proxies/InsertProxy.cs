using DbStatute.Fundamentals.Proxies;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Interfaces.Queries;
using DbStatute.Qualifiers;
using System;

namespace DbStatute.Proxies
{
    public class InsertProxy : InsertProxyBase, IInsertProxy
    {
        public InsertProxy()
        {
            InsertedFieldQualifier = new FieldQualifier();
        }

        public InsertProxy(IFieldQualifier insertedFieldQualifier)
        {
            InsertedFieldQualifier = insertedFieldQualifier ?? throw new ArgumentNullException(nameof(insertedFieldQualifier));
        }

        public IFieldQualifier InsertedFieldQualifier { get; }
    }

    public class InsertProxy<TModel> : InsertProxyBase<TModel>, IInsertProxy<TModel>
        where TModel : class, IModel, new()
    {
        public InsertProxy()
        {
            InsertedFieldQualifier = new FieldQualifier<TModel>();
        }

        public InsertProxy(IFieldQualifier<TModel> ınsertedFieldQualifier)
        {
            InsertedFieldQualifier = ınsertedFieldQualifier ?? throw new ArgumentNullException(nameof(ınsertedFieldQualifier));
        }

        public IFieldQualifier<TModel> InsertedFieldQualifier { get; }

        IFieldQualifier IInsertProxy.InsertedFieldQualifier => InsertedFieldQualifier;
    }
}