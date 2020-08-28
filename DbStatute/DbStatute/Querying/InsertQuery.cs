using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using DbStatute.Interfaces.Querying.Builders;
using System;

namespace DbStatute.Querying
{
    public class InsertQuery : IInsertQuery
    {
        public InsertQuery(IModelBuilder modelQueryQualifier)
        {
            ModelQueryQualifier = modelQueryQualifier ?? throw new ArgumentNullException(nameof(modelQueryQualifier));
        }

        public IModelBuilder ModelQueryQualifier { get; }
    }

    public class InsertQuery<TModel> : IInsertQuery<TModel>
        where TModel : class, IModel, new()
    {
        public InsertQuery()
        {
            ModelQueryQualifier = new ModelQueryQualifier<TModel>();
        }

        public InsertQuery(IModelBuilder<TModel> modelQueryQualifier)
        {
            ModelQueryQualifier = modelQueryQualifier ?? throw new ArgumentNullException(nameof(modelQueryQualifier));
        }

        public IModelBuilder<TModel> ModelQueryQualifier { get; }
        IModelBuilder IInsertQuery.ModelQueryQualifier => ModelQueryQualifier;
    }
}