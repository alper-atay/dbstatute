﻿using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using System;

namespace DbStatute
{
    public abstract class MultipleDeleteByQuery<TId, TModel, TSelectQuery, TMultipleSelectByQuery> : MultipleDelete<TId, TModel, TMultipleSelectByQuery>, IMultipleDeleteByQuery<TId, TModel, TSelectQuery, TMultipleSelectByQuery>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
        where TSelectQuery : ISelectQuery<TId, TModel>
        where TMultipleSelectByQuery : MultipleSelectByQuery<TId, TModel, TSelectQuery>
    {
        protected MultipleDeleteByQuery(TMultipleSelectByQuery multipleSelectByQuery) : base(multipleSelectByQuery)
        {
        }

        public TMultipleSelectByQuery MultipleSelectByQuery => MultipleSelect;
    }
}