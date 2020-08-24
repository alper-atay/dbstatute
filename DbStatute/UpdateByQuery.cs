using DbStatute.Interfaces;
using DbStatute.Querying;
using System;

namespace DbStatute
{
    public abstract class UpdateByQuery<TId, TModel, TUpdateQuery> : Update
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
        where TUpdateQuery : UpdateQuery<TId, TModel>
    {
        protected UpdateByQuery(TUpdateQuery updateQuery)
        {
            UpdateQuery = updateQuery ?? throw new ArgumentNullException(nameof(updateQuery));
        }

        public TUpdateQuery UpdateQuery { get; }
    }
}