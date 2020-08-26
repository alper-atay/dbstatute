using DbStatute.Interfaces.Querying.Statutes;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Interfaces
{
    public interface IMultipleDeleteByQuery<TModel, TSelectQuery, TMultipleSelectByQuery>
        where TModel : class, IModel, new()
        where TSelectQuery : ISelectQuery<TModel>
        where TMultipleSelectByQuery : IMultipleSelectByQuery<TModel, TSelectQuery>
    {
        TMultipleSelectByQuery MultipleSelectByQuery { get; }

        Task<int> DeleteAsync(IDbConnection dbConnection);
    }
}