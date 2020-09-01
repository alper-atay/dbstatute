using Basiclog;
using RepoDb;
using System.Collections.Generic;

namespace DbStatute.Interfaces.Fundamentals.Maps
{
    public interface IReadOnlyFieldPredicateMap
    {
        IReadOnlyDictionary<Field, ReadOnlyLogbookPredicate<object>> ReadOnlyFieldPredicateMap { get; }
    }
}