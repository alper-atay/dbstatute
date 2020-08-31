using Basiclog;
using RepoDb;
using System.Collections.Generic;

namespace DbStatute.Interfaces.Querying.Fundamentals.Maps
{
    public interface IReadOnlyFieldPredicateMap
    {
        IReadOnlyDictionary<Field, ReadOnlyLogbookPredicate<object>> ReadOnlyFieldPredicateMap { get; }
    }
}