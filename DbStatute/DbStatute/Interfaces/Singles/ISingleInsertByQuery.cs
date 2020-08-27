﻿using DbStatute.Interfaces.Fundamentals.Singles;
using DbStatute.Interfaces.Querying;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleInsertByQuery<TInsertQuery> : ISingleInsertBase
        where TInsertQuery : IInsertQuery
    {
        TInsertQuery InsertQuery { get; }
    }

    public interface ISingleInsertByQuery<TModel, TInsertQuery> : ISingleInsertBase<TModel>, ISingleInsertByQuery<TInsertQuery>
        where TModel : class, IModel, new()
        where TInsertQuery : IInsertQuery<TModel>
    {
        new TInsertQuery InsertQuery { get; }
    }
}