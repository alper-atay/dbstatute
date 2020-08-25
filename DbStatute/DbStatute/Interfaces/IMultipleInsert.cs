using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Interfaces
{
    public interface IMultipleInsert<TId, TModel>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        IEnumerable<TModel> InsertedModels { get; }
        IEnumerable<TModel> RawModels { get; }

        Task<IEnumerable<TModel>> InsertAsync(IDbConnection dbConnection);

        IAsyncEnumerable<TModel> InsertAsSingleAsync(IDbConnection dbConnection);
    }
}