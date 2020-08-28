using RepoDb;
using System.Collections.Generic;

namespace DbStatute.Interfaces.Querying.Fundamentals.Qualifiers
{
    public interface IFieldValueMap
    {
        IReadOnlyDictionary<Field, object> FieldValueMap { get; }
    }
}
