using DbStatute.Fundamentals.Proxies;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Interfaces.Qualifiers.Groups;
using DbStatute.Qualifiers;
using DbStatute.Qualifiers.Groups;
using System;

namespace DbStatute.Proxies
{
    public class InsertProxy : ProxyBase, IInsertProxy
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

    public class InsertProxy<TModel> : ProxyBase<TModel>, IInsertProxy<TModel>
        where TModel : class, IModel, new()
    {
        public InsertProxy()
        {
            InsertedFieldQualifier = new FieldQualifier<TModel>();
            ModelQualifierGroup = new ModelQualifierGroup<TModel>();
        }

        public InsertProxy(IModelQualifierGroup<TModel> modelQualifierGroup, IFieldQualifier<TModel> insertedFieldQualifier)
        {
            InsertedFieldQualifier = insertedFieldQualifier ?? throw new ArgumentNullException(nameof(insertedFieldQualifier));
            ModelQualifierGroup = modelQualifierGroup ?? throw new ArgumentNullException(nameof(modelQualifierGroup));
        }

        public IFieldQualifier<TModel> InsertedFieldQualifier { get; }

        IFieldQualifier IInsertProxy.InsertedFieldQualifier => InsertedFieldQualifier;

        public IModelQualifierGroup<TModel> ModelQualifierGroup { get; }

        IModelQualifierGroup IInsertProxy.ModelQualifierGroup => ModelQualifierGroup;
    }
}