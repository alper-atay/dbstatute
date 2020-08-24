using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using RepoDb;
using System;

namespace DbStatute.Querying
{
    public abstract class BuildableSelectQuery<TId, TModel> : SelectQuery<TId, TModel>, IBuildableSelectQuery
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        public override QueryGroup QueryGroup => Build();

        public abstract QueryGroup Build();
    }
}