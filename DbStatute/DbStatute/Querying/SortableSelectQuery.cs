using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using RepoDb;
using System;
using System.Collections.Generic;

namespace DbStatute.Querying
{
    public abstract class SortableSelectQuery<TId, TModel> : SelectQuery<TId, TModel>, ISortableQuery
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        public abstract bool HasOrderField { get; }
        public abstract IEnumerable<OrderField> OrderFields { get; }
    }
}