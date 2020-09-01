using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Interfaces.Fundamentals
{
    public interface IMultipleDeleteBase : IDeleteBase
    {
        IEnumerable<object> DeletedModels { get; }

        IAsyncEnumerable<object> DeleteAsSinglyAsync(IDbConnection dbConnection, bool allowNullReturnIfDeleted = false);

        Task<IEnumerable<object>> DeleteAsync(IDbConnection dbConnection);
    }

    public interface IMultipleDeleteBase<TModel> : IDeleteBase<TModel>, IMultipleDeleteBase
        where TModel : class, IModel, new()
    {
        new IEnumerable<TModel> DeletedModels { get; }

        new IAsyncEnumerable<TModel> DeleteAsSinglyAsync(IDbConnection dbConnection, bool allowNullReturnIfDeleted = false);

        new Task<IEnumerable<TModel>> DeleteAsync(IDbConnection dbConnection);
    }
}