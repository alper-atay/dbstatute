using DbStatute.Interfaces.Utilities;
using RepoDb;
using System.Collections.Generic;

namespace DbStatute.Interfaces.Qualifiers
{
    public interface IQueryFieldQualifier : ISettableQueryField
    {
        bool HasQueryField { get; }
        IEnumerable<QueryField> ReadOnlyQueryFields { get; }
    }

    public interface IQueryFieldQualifier<TModel> : ISettableQueryField<TModel>
        where TModel : class, IModel, new()
    {
    }
}