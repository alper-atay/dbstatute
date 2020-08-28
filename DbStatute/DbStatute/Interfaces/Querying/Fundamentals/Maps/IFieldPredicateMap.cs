using Basiclog;
using RepoDb;
using System.Collections.Generic;

namespace DbStatute.Interfaces.Querying.Fundamentals.Qualifiers
{
    public interface IFieldPredicateMap
    {
        IReadOnlyDictionary<Field, ReadOnlyLogbookPredicate<object>> FieldPredicateMap { get; }
    }
}