using DbStatute.Fundamentals.Proxies;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Interfaces.Qualifiers.Groups;
using DbStatute.Qualifiers;
using DbStatute.Qualifiers.Group;
using System;

namespace DbStatute.Proxies
{
    public class UpdateProxy : StatuteProxyBase, IUpdateProxy
    {
        public UpdateProxy()
        {
            ModelQualifierGroup = new ModelQualifierGroup();
            MergedFieldQualifier = new FieldQualifier();
        }

        public UpdateProxy(IModelQualifierGroup modelQualifierGroup, IFieldQualifier fieldQualifier)
        {
            ModelQualifierGroup = modelQualifierGroup ?? throw new ArgumentNullException(nameof(modelQualifierGroup));
            MergedFieldQualifier = fieldQualifier ?? throw new ArgumentNullException(nameof(fieldQualifier));
        }

        public IFieldQualifier MergedFieldQualifier { get; }

        public IModelQualifierGroup ModelQualifierGroup { get; }
    }

    public class UpdateProxy<TModel> : StatuteProxyBase<TModel>, IUpdateProxy<TModel>
        where TModel : class, IModel, new()
    {
        public UpdateProxy()
        {
            ModelQualifierGroup = new ModelQualifierGroup<TModel>();
            MergedFieldQualifier = new FieldQualifier<TModel>();
        }

        public UpdateProxy(IModelQualifierGroup<TModel> modelQualifierGroup, IFieldQualifier<TModel> fieldQualifier)
        {
            ModelQualifierGroup = modelQualifierGroup ?? throw new ArgumentNullException(nameof(modelQualifierGroup));
            MergedFieldQualifier = fieldQualifier ?? throw new ArgumentNullException(nameof(fieldQualifier));
        }

        public IFieldQualifier<TModel> MergedFieldQualifier { get; }

        IFieldQualifier IUpdateProxy.MergedFieldQualifier => MergedFieldQualifier;

        public IModelQualifierGroup<TModel> ModelQualifierGroup { get; }

        IModelQualifierGroup IUpdateProxy.ModelQualifierGroup => ModelQualifierGroup;
    }
}