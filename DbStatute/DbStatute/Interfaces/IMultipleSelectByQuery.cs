﻿using DbStatute.Interfaces.Querying;
using System;

namespace DbStatute.Interfaces
{
    public interface IMultipleSelectByQuery<TId, TModel, TSelectQuery> : IMultipleSelect<TId, TModel>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
        where TSelectQuery : ISelectQuery<TId, TModel>
    {
        TSelectQuery SelectQuery { get; }
    }
}