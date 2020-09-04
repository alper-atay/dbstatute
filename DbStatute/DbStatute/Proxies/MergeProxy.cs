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
    public class MergeProxy : StatuteProxyBase, IMergeProxy
    {
        public MergeProxy()
        {
            ModelQualifierGroup = new ModelQualifierGroup();
            MergedFieldQualifier = new FieldQualifier();
        }

        public MergeProxy(IModelQualifierGroup modelQualifierGroup, IFieldQualifier mergedFieldQualifier)
        {
            ModelQualifierGroup = modelQualifierGroup ?? throw new ArgumentNullException(nameof(modelQualifierGroup));
            MergedFieldQualifier = mergedFieldQualifier ?? throw new ArgumentNullException(nameof(mergedFieldQualifier));
        }

        public IFieldQualifier MergedFieldQualifier { get; }

        public IModelQualifierGroup ModelQualifierGroup { get; }
    }

    public class MergeProxy<TModel> : StatuteProxyBase<TModel>, IMergeProxy<TModel>
        where TModel : class, IModel, new()
    {
        public MergeProxy()
        {
            ModelQualifierGroup = new ModelQualifierGroup<TModel>();
            MergedFieldQualifier = new FieldQualifier<TModel>();
        }

        public MergeProxy(IModelQualifierGroup<TModel> modelQualifierGroup, IFieldQualifier<TModel> mergedFieldQualifier)
        {
            ModelQualifierGroup = modelQualifierGroup ?? throw new ArgumentNullException(nameof(modelQualifierGroup));
            MergedFieldQualifier = mergedFieldQualifier ?? throw new ArgumentNullException(nameof(mergedFieldQualifier));
        }

        public IFieldQualifier<TModel> MergedFieldQualifier { get; }

        IFieldQualifier IMergeProxy.MergedFieldQualifier => MergedFieldQualifier;

        public IModelQualifierGroup<TModel> ModelQualifierGroup { get; }

        IModelQualifierGroup IMergeProxy.ModelQualifierGroup => ModelQualifierGroup;
    }
}