using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using DbStatute.Interfaces.Querying.Builders;
using System;

namespace DbStatute.Querying
{
    public class UpdateQuery : StatuteQueryBase, IUpdateQuery
    {
        public UpdateQuery(IModelBuilder modelQueryQualifier)
        {
            ModelQueryQualifier = modelQueryQualifier ?? throw new ArgumentNullException(nameof(modelQueryQualifier));
        }

        public IModelBuilder ModelQueryQualifier { get; }
    }

    public class UpdateQuery<TModel> : StatuteQueryBase<TModel>, IUpdateQuery<TModel>
        where TModel : class, IModel, new()
    {
        public UpdateQuery()
        {
            ModelQueryQualifier = new ModelQueryQualifier<TModel>();
        }

        public UpdateQuery(IModelBuilder<TModel> modelQueryQualifier)
        {
            ModelQueryQualifier = modelQueryQualifier ?? throw new ArgumentNullException(nameof(modelQueryQualifier));
        }

        public IModelBuilder<TModel> ModelQueryQualifier { get; }
        IModelBuilder IUpdateQuery.ModelQueryQualifier => ModelQueryQualifier;
    }
}