using DbStatute.Fundamentals.Proxies;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Querying.Builders;
using DbStatute.Interfaces.Querying.Qualifiers.Fields;
using DbStatute.Querying.Builders;
using DbStatute.Querying.Qualifiers;
using System;

namespace DbStatute.Querying
{
    public class InsertProxy : StatuteProxyBase, IInsertProxy
    {
        public InsertProxy(IModelBuilder modelBuilder)
        {
            ModelBuilder = modelBuilder ?? throw new ArgumentNullException(nameof(modelBuilder));
            InsertedFieldQualifier = new FieldQualifier();
        }

        public InsertProxy(IModelBuilder modelBuilder, IFieldQualifier insertedFieldQualifier)
        {
            ModelBuilder = modelBuilder ?? throw new ArgumentNullException(nameof(modelBuilder));
            InsertedFieldQualifier = insertedFieldQualifier ?? throw new ArgumentNullException(nameof(insertedFieldQualifier));
        }

        public IFieldQualifier InsertedFieldQualifier { get; }
        public IModelBuilder ModelBuilder { get; }
    }

    public class InsertProxy<TModel> : StatuteProxyBase<TModel>, IInsertProxy<TModel>
        where TModel : class, IModel, new()
    {
        public InsertProxy()
        {
            ModelBuilder = new ModelBuilder<TModel>();
            InsertedFieldQualifier = new FieldQualifier<TModel>();
        }

        public InsertProxy(IModelBuilder<TModel> modelBuilder)
        {
            ModelBuilder = modelBuilder ?? throw new ArgumentNullException(nameof(modelBuilder));
            InsertedFieldQualifier = new FieldQualifier<TModel>();
        }

        public InsertProxy(IModelBuilder<TModel> modelBuilder, IFieldQualifier<TModel> insertedFieldQualifier)
        {
            ModelBuilder = modelBuilder ?? throw new ArgumentNullException(nameof(modelBuilder));
            InsertedFieldQualifier = insertedFieldQualifier ?? throw new ArgumentNullException(nameof(insertedFieldQualifier));
        }

        public IFieldQualifier<TModel> InsertedFieldQualifier { get; }
        IFieldQualifier IInsertProxy.InsertedFieldQualifier => InsertedFieldQualifier;
        public IModelBuilder<TModel> ModelBuilder { get; }
        IModelBuilder IInsertProxy.ModelBuilder => ModelBuilder;
    }
}