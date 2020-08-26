﻿namespace DbStatute.Interfaces.Querying.Statutes
{
    public interface IDeleteQuery : IStatuteQuery
    {
        ISelectQuery SelectQuery { get; }
    }

    public interface IDeleteQuery<TModel> : IStatuteQuery<TModel>, IDeleteQuery
        where TModel : class, IModel, new()
    {
        new ISelectQuery<TModel> SelectQuery { get; }
    }
}