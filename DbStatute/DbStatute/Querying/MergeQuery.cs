using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using DbStatute.Interfaces.Querying.Qualifiers;
using DbStatute.Querying.Qualifiers;
using System;

namespace DbStatute.Querying
{
    public class MergeQuery : StatuteQueryBase, IMergeQuery
    {
        public MergeQuery(IModelQueryQualifier modelQueryQualifier)
        {
            ModelQueryQualifier = modelQueryQualifier ?? throw new ArgumentNullException(nameof(modelQueryQualifier));
        }

        public IModelQueryQualifier ModelQueryQualifier { get; }
    }

    public class MergeQuery<TModel> : StatuteQueryBase<TModel>, IMergeQuery<TModel>
        where TModel : class, IModel, new()
    {
        public MergeQuery()
        {
            ModelQueryQualifier = new ModelQueryQualifier<TModel>();
        }

        public MergeQuery(IModelQueryQualifier<TModel> modelQueryQualifier)
        {
            ModelQueryQualifier = modelQueryQualifier ?? throw new ArgumentNullException(nameof(modelQueryQualifier));
        }

        public IModelQueryQualifier<TModel> ModelQueryQualifier { get; }
        IModelQueryQualifier IMergeQuery.ModelQueryQualifier => ModelQueryQualifier;
    }
}