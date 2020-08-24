using Basiclog;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using RepoDb;
using System;

namespace DbStatute.Querying
{
    public abstract class SelectQuery<TId, TModel> : ISelectQuery<TId, TModel>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        public abstract QueryGroup QueryGroup { get; }

        public TModel SelecterModel { get; } = new TModel();

        public abstract IReadOnlyLogbook Test();
    }
}