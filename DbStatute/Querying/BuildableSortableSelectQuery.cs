using DbStatute.Interfaces.Querying;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DbStatute.Querying
{
    public abstract class BuildableSortableSelectQuery<TId> : SelectQuery<TId>, IBuildableQuery, ISortableQuery
        where TId : struct, IConvertible
    {
        public bool HasOrderField => !(OrderFields is null) && OrderFields.Count() > 0;
        public abstract IEnumerable<OrderField> OrderFields { get; }
        public override QueryGroup QueryGroup => Build();

        public abstract QueryGroup Build();
    }
}