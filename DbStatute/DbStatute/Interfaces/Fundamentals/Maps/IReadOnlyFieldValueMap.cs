using RepoDb;
using System.Collections.Generic;

namespace DbStatute.Interfaces.Fundamentals.Maps
{
    public interface IReadOnlyFieldValueMap
    {
        IReadOnlyDictionary<Field, object> ReadOnlyFieldValueMap { get; }
    }
}
