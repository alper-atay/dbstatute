using RepoDb;
using RepoDb.Enumerations;
using System.Collections.Generic;

namespace DbStatute.Interfaces.Fundamentals.Enumerables
{
    public interface IFieldOperationPairs : IReadOnlyDictionary<Field, Operation>
    {
    }
}