using DbStatute.Interfaces.Utilities;
using RepoDb;
using System.Collections.Generic;

namespace DbStatute.Interfaces.Querying.Qualifiers.Fields
{
    public interface IFieldQualifier : ISettableField
    {
        IEnumerable<Field> ReadOnlyFields { get; }
        bool HasField { get; }
    }

    public interface IFieldQualifier<TModel> : ISettableField<TModel>, IFieldQualifier
        where TModel : class, IModel, new()
    {
    }
}