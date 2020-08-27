using DbStatute.Interfaces.Querying;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Interfaces.Multiples
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