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
    public class MergeProxy : ProxyBase, IMergeProxy
    {
        public MergeProxy()
        {
            ModelQuery = new ModelQuery();
            MergedFieldQualifier = new FieldQualifier();
        }

        public MergeProxy(IModelQuery modelQuery, IFieldQualifier mergedFieldQualifier)
        {
            ModelQuery = modelQuery ?? throw new ArgumentNullException(nameof(modelQuery));
            MergedFieldQualifier = mergedFieldQualifier ?? throw new ArgumentNullException(nameof(mergedFieldQualifier));
        }

        public IFieldQualifier MergedFieldQualifier { get; }

        public IModelQuery ModelQuery { get; }
    }

    public class MergeProxy<TModel> : ProxyBase<TModel>, IMergeProxy<TModel>
        where TModel : class, IModel, new()
    {
        public MergeProxy()
        {
            ModelQuery = new ModelQuery<TModel>();
            MergedFieldQualifier = new FieldQualifier<TModel>();
        }

        public MergeProxy(IModelQuery<TModel> modelQuery, IFieldQualifier<TModel> mergedFieldQualifier)
        {
            ModelQuery = modelQuery ?? throw new ArgumentNullException(nameof(modelQuery));
            MergedFieldQualifier = mergedFieldQualifier ?? throw new ArgumentNullException(nameof(mergedFieldQualifier));
        }

        public IFieldQualifier<TModel> MergedFieldQualifier { get; }

        IFieldQualifier IMergeProxy.MergedFieldQualifier => MergedFieldQualifier;

        public IModelQuery<TModel> ModelQuery { get; }

        IModelQuery IMergeProxy.ModelQuery => ModelQuery;
    }
}