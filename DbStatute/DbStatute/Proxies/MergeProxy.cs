using DbStatute.Builders;
using DbStatute.Fundamentals.Proxies;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Builders;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Qualifiers;
using System;

namespace DbStatute.Querying
{
    public class MergeProxy : StatuteProxyBase, IMergeProxy
    {
        public MergeProxy(IModelBuilder modelBuilder)
        {
            ModelBuilder = modelBuilder;
        }

        public IFieldQualifier MergedFieldQualifier { get; }

        public IModelBuilder ModelBuilder { get; }
    }

    public class MergeProxy<TModel> : StatuteProxyBase<TModel>, IMergeProxy<TModel>
        where TModel : class, IModel, new()
    {
        public MergeProxy()
        {
            ModelBuilder = new ModelBuilder<TModel>();
        }

        public MergeProxy(IModelBuilder<TModel> modelBuilder)
        {
            ModelBuilder = modelBuilder ?? throw new ArgumentNullException(nameof(modelBuilder));
        }

        public IFieldQualifier<TModel> MergedFieldQualifier { get; }

        IFieldQualifier IMergeProxy.MergedFieldQualifier => MergedFieldQualifier;

        public IModelBuilder<TModel> ModelBuilder { get; }

        IModelBuilder IMergeProxy.ModelBuilder => ModelBuilder;
    }
}