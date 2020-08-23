using Basiclog;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using RepoDb;
using System;

namespace DbStatute.Querying
{
    public abstract class SelectQuery<TId, TModel> : ISelectQuery<TId, TModel>
        where TId : struct, IConvertible
        where TModel : class, IModel<TId>
    {
        public abstract QueryGroup QueryGroup { get; }

        public TModel Model { get; }

        public abstract IReadOnlyLogbook Test();
    }
}