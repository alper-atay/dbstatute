using DbStatute.Interfaces.Utilities;
using RepoDb;
using System.Collections.Generic;

namespace DbStatute.Interfaces.Querying.Qualifiers.Fields
{
    public interface IQueryFieldQualifier : ISettableQueryField
    {
        bool HasQueryField { get; }
        IEnumerable<QueryField> QueryFields { get; }
    }

    public interface IQueryFieldQualifier<TModel> : ISettableQueryField<TModel>
        where TModel : class, IModel, new()
    {
    }
}