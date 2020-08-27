using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using DbStatute.Interfaces.Querying.Qualifiers;
using DbStatute.Querying.Qualifiers;
using System;

namespace DbStatute.Querying
{
    public class InsertQuery : IInsertQuery
    {
        public InsertQuery(IModelQueryQualifier modelQueryQualifier)
        {
            ModelQueryQualifier = modelQueryQualifier ?? throw new ArgumentNullException(nameof(modelQueryQualifier));
        }

        public IModelQueryQualifier ModelQueryQualifier { get; }
    }

    public class InsertQuery<TModel> : IInsertQuery<TModel>
        where TModel : class, IModel, new()
    {
        public InsertQuery()
        {
            ModelQueryQualifier = new ModelQueryQualifier<TModel>();
        }

        public InsertQuery(IModelQueryQualifier<TModel> modelQueryQualifier)
        {
            ModelQueryQualifier = modelQueryQualifier ?? throw new ArgumentNullException(nameof(modelQueryQualifier));
        }

        public IModelQueryQualifier<TModel> ModelQueryQualifier { get; }
        IModelQueryQualifier IInsertQuery.ModelQueryQualifier => ModelQueryQualifier;
    }
}