using RepoDb;
using System.Collections.Generic;

namespace DbStatute.Interfaces.Querying.Fundamentals.Maps
{
    public interface IFieldValueMap
    {
        IReadOnlyDictionary<Field, object> FieldValueMap { get; }
    }
}
