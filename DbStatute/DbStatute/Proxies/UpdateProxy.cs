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
    public class UpdateProxy : StatuteProxyBase, IUpdateProxy
    {
        public UpdateProxy(IModelBuilder modelBuilder, IFieldQualifier fieldQualifier)
        {
            ModelBuilder = modelBuilder ?? throw new ArgumentNullException(nameof(modelBuilder));
            FieldQualifier = fieldQualifier ?? throw new ArgumentNullException(nameof(fieldQualifier));
        }

        public IFieldQualifier FieldQualifier { get; }
        public IModelBuilder ModelBuilder { get; }
    }

    public class UpdateProxy<TModel> : StatuteProxyBase<TModel>, IUpdateProxy<TModel>
        where TModel : class, IModel, new()
    {
        public UpdateProxy()
        {
            ModelBuilder = new ModelBuilder<TModel>();
            FieldQualifier = new FieldQualifier<TModel>();
        }

        public UpdateProxy(IModelBuilder<TModel> modelBuilder, IFieldQualifier<TModel> fieldQualifier)
        {
            ModelBuilder = modelBuilder ?? throw new ArgumentNullException(nameof(modelBuilder));
            FieldQualifier = fieldQualifier ?? throw new ArgumentNullException(nameof(fieldQualifier));
        }

        public IFieldQualifier<TModel> FieldQualifier { get; }
        IFieldQualifier IUpdateProxy.FieldQualifier => FieldQualifier;
        public IModelBuilder<TModel> ModelBuilder { get; }
        IModelBuilder IUpdateProxy.ModelBuilder => ModelBuilder;
    }
}