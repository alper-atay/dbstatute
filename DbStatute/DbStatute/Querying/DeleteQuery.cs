﻿using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;

namespace DbStatute.Querying
{
    public class DeleteQuery : StatuteQueryBase, IDeleteQuery
    {
        public DeleteQuery()
        {
            SelectQuery = new SelectQuery();
        }

        public DeleteQuery(ISelectQuery selectQuery)
        {
            SelectQuery = selectQuery;
        }

        public ISelectQuery SelectQuery { get; }
    }

    public class DeleteQuery<TModel> : StatuteQueryBase<TModel>, IDeleteQuery<TModel>
        where TModel : class, IModel, new()
    {
        public DeleteQuery()
        {
            SelectQuery = new SelectQuery<TModel>();
        }

        public ISelectQuery<TModel> SelectQuery { get; }
        ISelectQuery IDeleteQuery.SelectQuery => SelectQuery;
    }
}