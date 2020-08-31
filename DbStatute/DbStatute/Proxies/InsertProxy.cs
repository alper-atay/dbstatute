using DbStatute.Fundamentals.Proxies;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Querying.Builders;
using DbStatute.Querying.Builders;
using System;

namespace DbStatute.Querying
{
    public class InsertProxy : StatuteProxyBase, IInsertProxy
    {
        public InsertProxy(IModelBuilder modelBuilder)
        {
            ModelBuilder = modelBuilder ?? throw new ArgumentNullException(nameof(modelBuilder));
        }

        public IModelBuilder ModelBuilder { get; }
    }

    public class InsertProxy<TModel> : StatuteProxyBase<TModel>, IInsertProxy<TModel>
        where TModel : class, IModel, new()
    {
        public InsertProxy()
        {
            ModelBuilder = new ModelBuilder<TModel>();
        }

        public InsertProxy(IModelBuilder<TModel> modelBuilder)
        {
            ModelBuilder = modelBuilder;
        }

        public IModelBuilder<TModel> ModelBuilder { get; }

        IModelBuilder IInsertProxy.ModelBuilder => ModelBuilder;
    }
}