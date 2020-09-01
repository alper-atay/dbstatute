using RepoDb;
using System.Collections.Generic;

namespace DbStatute.Interfaces.Fundamentals.Enumerables
{
    public interface IFieldValuePairs
    {
        IReadOnlyDictionary<Field, object> FieldValuePairs { get; }
    }
}
