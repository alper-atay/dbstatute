using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using System;

namespace DbStatute
{
    public abstract class MergeByQuery<TId, TModel, TUpdateQuery> : Merge
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
        where TUpdateQuery : IUpdateQuery<TId, TModel>
    {
        protected MergeByQuery(TUpdateQuery updateQuery)
        {
            UpdateQuery = updateQuery ?? throw new ArgumentNullException(nameof(updateQuery));
        }

        public TUpdateQuery UpdateQuery { get; }
    }
}