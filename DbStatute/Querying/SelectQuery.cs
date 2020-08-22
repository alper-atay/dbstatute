using Basiclog;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using RepoDb;
using System;

namespace DbStatute.Querying
{
    public abstract class SelectQuery<TId> : ISelectQuery<TId>
        where TId : struct, IConvertible
    {
        public abstract TId Id { get; set; }
        public abstract QueryGroup QueryGroup { get; }

        public abstract bool Equals(IIdentifiable<TId> other);

        public abstract IReadOnlyLogbook Test();
    }
}