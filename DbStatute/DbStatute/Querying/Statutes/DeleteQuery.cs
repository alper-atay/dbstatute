using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying.Statutes;
using System;

namespace DbStatute.Querying.Statutes
{
    public class DeleteQuery : IDeleteQuery
    {
        public DeleteQuery()
        {

        }

        public ISelectQuery SelectQuery { get; }
    }

    public class DeleteQuery<TId, TModel> : StatuteQuery, IDeleteQuery<TId, TModel>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        public DeleteQuery()
        {
            SelectQuery = new SelectQuery<TId, TModel>();
        }

        public ISelectQuery<TId, TModel> SelectQuery { get; }

        ISelectQuery IDeleteQuery.SelectQuery => SelectQuery;
    }
}