using RepoDb;
using RepoDb.Enumerations;
using System.Collections.Generic;

namespace DbStatute.Interfaces.Querying.Fundamentals.Qualifiers
{
    public interface IFieldOperationMap
    {
        IReadOnlyDictionary<Field, Operation> FieldOperationMap { get; }
    }
}
