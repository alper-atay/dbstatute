using DbStatute.Interfaces.Querying;
using RepoDb;
using System;
using System.Collections.Generic;

namespace DbStatute.Querying
{
    public abstract class SortableSelectQuery<TId> : SelectQuery<TId>, ISortableQuery
        where TId : struct, IConvertible
    {
        public abstract bool HasOrderField { get; }
        public abstract IEnumerable<OrderField> OrderFields { get; }
    }
}