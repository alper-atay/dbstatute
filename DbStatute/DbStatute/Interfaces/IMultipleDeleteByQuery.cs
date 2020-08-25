using DbStatute.Interfaces.Querying;
using System;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Interfaces
{
    public interface IMultipleDeleteByQuery<TId, TModel, TSelectQuery, TMultipleSelectByQuery>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
        where TSelectQuery : ISelectQuery<TId, TModel>
        where TMultipleSelectByQuery : IMultipleSelectByQuery<TId, TModel, TSelectQuery>
    {
        TMultipleSelectByQuery MultipleSelectByQuery { get; }

        Task<int> DeleteAsync(IDbConnection dbConnection);
    }
}