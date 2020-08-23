using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using RepoDb;
using System;

namespace DbStatute.Querying
{
    public abstract class BuildableSelectQuery<TId, TModel> : SelectQuery<TId, TModel>, IBuildableSelectQuery
        where TId : struct, IConvertible
        where TModel : class, IModel<TId>
    {
        public override QueryGroup QueryGroup => Build();

        public abstract QueryGroup Build();
    }
}