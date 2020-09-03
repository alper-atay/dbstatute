using DbStatute.Fundamentals.Proxies;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Interfaces.Qualifiers.Groups;
using DbStatute.Qualifiers;
using DbStatute.Qualifiers.Group;
using System;

namespace DbStatute.Querying
{
    public class InsertProxy : StatuteProxyBase, IInsertProxy
    {
        public InsertProxy()
        {
            ModelQualifierGroup = new ModelQualifierGroup();
            InsertedFieldQualifier = new FieldQualifier();
        }

        public InsertProxy(IFieldQualifier insertedFieldQualifier)
        {
            InsertedFieldQualifier = insertedFieldQualifier ?? throw new ArgumentNullException(nameof(insertedFieldQualifier));
        }

        public IFieldQualifier InsertedFieldQualifier { get; }

        public IModelQualifierGroup ModelQualifierGroup { get; }
    }

    public class InsertProxy<TModel> : StatuteProxyBase<TModel>, IInsertProxy<TModel>
        where TModel : class, IModel, new()
    {
        public InsertProxy()
        {
            InsertedFieldQualifier = new FieldQualifier<TModel>();
        }

        public InsertProxy(IFieldQualifier<TModel> insertedFieldQualifier)
        {
            InsertedFieldQualifier = insertedFieldQualifier ?? throw new ArgumentNullException(nameof(insertedFieldQualifier));
        }

        public IFieldQualifier<TModel> InsertedFieldQualifier { get; }

        IFieldQualifier IInsertProxy.InsertedFieldQualifier => InsertedFieldQualifier;

        public IModelQualifierGroup<TModel> ModelQualifierGroup { get; }

        IModelQualifierGroup IInsertProxy.ModelQualifierGroup => ModelQualifierGroup;
    }
}