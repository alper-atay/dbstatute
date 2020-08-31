using RepoDb;
using System.Collections.Generic;

namespace DbStatute.Interfaces.Querying.Fundamentals.Maps
{
    public interface IReadOnlyFieldValueMap
    {
        IReadOnlyDictionary<Field, object> ReadOnlyFieldValueMap { get; }
    }
}
