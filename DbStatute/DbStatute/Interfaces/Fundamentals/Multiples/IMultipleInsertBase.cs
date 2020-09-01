using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Interfaces.Fundamentals.Multiples
{
    public interface IMultipleInsertBase : IInsertBase
    {
        int BatchSize { get; set; }

        IEnumerable<object> InsertedModels { get; }

        IAsyncEnumerable<object> InsertAsSingleAsync(IDbConnection dbConnection);

        Task<IEnumerable<object>> InsertAsync(IDbConnection dbConnection);
    }

    public interface IMultipleInsertBase<TModel> : IInsertBase<TModel>, IMultipleInsertBase
        where TModel : class, IModel, new()
    {
        new IEnumerable<TModel> InsertedModels { get; }

        new IAsyncEnumerable<TModel> InsertAsSingleAsync(IDbConnection dbConnection);

        new Task<IEnumerable<TModel>> InsertAsync(IDbConnection dbConnection);
    }
}