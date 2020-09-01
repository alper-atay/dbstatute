using Basiclog;
using RepoDb;
using System.Collections.Generic;

namespace DbStatute.Interfaces.Fundamentals.Enumerables
{
    public interface IFieldPredicatePairs
    {
        IReadOnlyDictionary<Field, ReadOnlyLogbookPredicate<object>> FieldPredicatePairs { get; }
    }
}