using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying.Statutes;

namespace DbStatute
{
    public abstract class MultipleDeleteByQuery<TModel, TSelectQuery, TMultipleSelectByQuery> : MultipleDelete<TModel, TMultipleSelectByQuery>, IMultipleDeleteByQuery<TModel, TSelectQuery, TMultipleSelectByQuery>

        where TModel : class, IModel, new()
        where TSelectQuery : ISelectQuery<TModel>
        where TMultipleSelectByQuery : IMultipleSelectByQuery<TModel, TSelectQuery>
    {
        protected MultipleDeleteByQuery(TMultipleSelectByQuery multipleSelectByQuery) : base(multipleSelectByQuery)
        {
        }

        public TMultipleSelectByQuery MultipleSelectByQuery => MultipleSelect;
    }
}