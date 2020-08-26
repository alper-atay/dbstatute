using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Interfaces
{
    public interface IMultipleInsert<TModel>
        where TModel : class, IModel, new()
    {
        IEnumerable<TModel> InsertedModels { get; }
        IEnumerable<TModel> RawModels { get; }

        IAsyncEnumerable<TModel> InsertAsSingleAsync(IDbConnection dbConnection);

        Task<IEnumerable<TModel>> InsertAsync(IDbConnection dbConnection);
    }
}