using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using System;

namespace DbStatute
{
    public abstract class MultipleDeleteByQuery<TId, TModel, TSelectQuery, TMultipleSelectByQuery> : MultipleDelete<TId, TModel, TMultipleSelectByQuery>
        where TId : struct, IConvertible
        where TModel : class, IModel<TId>
        where TSelectQuery : ISelectQuery<TId, TModel>
        where TMultipleSelectByQuery : MultipleSelectByQuery<TId, TModel, TSelectQuery>
    {
        protected MultipleDeleteByQuery(TMultipleSelectByQuery multipleSelectByQuery) : base(multipleSelectByQuery)
        {
        }

        public TMultipleSelectByQuery MultipleSelectByQuery => MultipleSelect;
    }
}