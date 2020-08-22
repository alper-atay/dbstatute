using DbStatute.Interfaces.Querying;
using RepoDb;
using System;

namespace DbStatute.Querying
{
    public abstract class BuildableSelectQuery<TId> : SelectQuery<TId>, IBuildableQuery
        where TId : struct, IConvertible
    {
        public override QueryGroup QueryGroup => Build();

        public abstract QueryGroup Build();
    }
}