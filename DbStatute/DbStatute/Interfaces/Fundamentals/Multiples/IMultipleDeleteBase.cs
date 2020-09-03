using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Interfaces.Fundamentals.Multiples
{
    public interface IMultipleDeleteBase : IDeleteBase
    {
        IEnumerable<object> DeletedModels { get; }

        IAsyncEnumerable<object> DeleteAsSinglyAsync(IDbConnection dbConnection);

        Task<IEnumerable<object>> DeleteAsync(IDbConnection dbConnection);
    }

    public interface IMultipleDeleteBase<TModel> : IDeleteBase<TModel>, IMultipleDeleteBase
        where TModel : class, IModel, new()
    {
        new IEnumerable<TModel> DeletedModels { get; }

        new IAsyncEnumerable<TModel> DeleteAsSinglyAsync(IDbConnection dbConnection);

        new Task<IEnumerable<TModel>> DeleteAsync(IDbConnection dbConnection);
    }
}