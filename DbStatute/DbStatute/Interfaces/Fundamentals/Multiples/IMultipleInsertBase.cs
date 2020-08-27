using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Interfaces.Fundamentals.Multiples
{
    public interface IMultipleInsert : IInsertBase
    {
        IEnumerable<object> InsertedModels { get; }

        IAsyncEnumerable<object> InsertAsSingleAsync(IDbConnection dbConnection);

        Task<IEnumerable<object>> InsertAsync(IDbConnection dbConnection);
    }

    public interface IMultipleInsertBase<TModel> : IInsertBase<TModel>
        where TModel : class, IModel, new()
    {
        IEnumerable<TModel> InsertedModels { get; }

        IAsyncEnumerable<TModel> InsertAsSingleAsync(IDbConnection dbConnection);

        Task<IEnumerable<TModel>> InsertAsync(IDbConnection dbConnection);
    }
}