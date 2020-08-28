using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using DbStatute.Interfaces.Querying.Builders;
using DbStatute.Querying.Qualifiers;
using System;

namespace DbStatute.Querying
{
    public class MergeQuery : StatuteQueryBase, IMergeQuery
    {
        public MergeQuery(IModelBuilder modelQueryQualifier)
        {
            ModelQueryQualifier = modelQueryQualifier ?? throw new ArgumentNullException(nameof(modelQueryQualifier));
        }

        public IModelBuilder ModelQueryQualifier { get; }
    }

    public class MergeQuery<TModel> : StatuteQueryBase<TModel>, IMergeQuery<TModel>
        where TModel : class, IModel, new()
    {
        public MergeQuery()
        {
            ModelQueryQualifier = new ModelQueryQualifier<TModel>();
        }

        public MergeQuery(IModelBuilder<TModel> modelQueryQualifier)
        {
            ModelQueryQualifier = modelQueryQualifier ?? throw new ArgumentNullException(nameof(modelQueryQualifier));
        }

        public IModelBuilder<TModel> ModelQueryQualifier { get; }
        IModelBuilder IMergeQuery.ModelQueryQualifier => ModelQueryQualifier;
    }
}