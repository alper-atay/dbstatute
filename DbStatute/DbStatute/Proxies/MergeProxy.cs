using DbStatute.Fundamentals.Proxies;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Querying.Builders;
using DbStatute.Querying.Builders;
using System;

namespace DbStatute.Querying
{
    public class MergeProxy : StatuteProxyBase, IMergeProxy
    {
        public MergeProxy(IModelBuilder modelBuilder)
        {
            ModelBuilder = modelBuilder;
        }

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

        public IModelBuilder<TModel> ModelBuilder { get; }
        IModelBuilder IMergeProxy.ModelBuilder => ModelBuilder;
    }
}