using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using DbStatute.Interfaces.Querying.Qualifiers;
using DbStatute.Querying.Qualifiers;
using System;

namespace DbStatute.Querying
{
    public class UpdateQuery : StatuteQueryBase, IUpdateQuery
    {
        public UpdateQuery(IModelQueryQualifier modelQueryQualifier)
        {
            ModelQueryQualifier = modelQueryQualifier ?? throw new ArgumentNullException(nameof(modelQueryQualifier));
        }

        public IModelQueryQualifier ModelQueryQualifier { get; }
    }

    public class UpdateQuery<TModel> : StatuteQueryBase<TModel>, IUpdateQuery<TModel>
        where TModel : class, IModel, new()
    {
        public UpdateQuery()
        {
            ModelQueryQualifier = new ModelQueryQualifier<TModel>();
        }

        public UpdateQuery(IModelQueryQualifier<TModel> modelQueryQualifier)
        {
            ModelQueryQualifier = modelQueryQualifier ?? throw new ArgumentNullException(nameof(modelQueryQualifier));
        }

        public IModelQueryQualifier<TModel> ModelQueryQualifier { get; }
        IModelQueryQualifier IUpdateQuery.ModelQueryQualifier => ModelQueryQualifier;
    }
}