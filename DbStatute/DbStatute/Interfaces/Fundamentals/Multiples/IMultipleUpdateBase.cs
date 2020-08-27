using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Interfaces.Fundamentals.Multiples
{
    public interface IMultipleUpdateBase : IUpdateBase
    {
        IEnumerable<object> UpdatedModels { get; }

        IAsyncEnumerable<object> UpdateAsSinglyAsync(IDbConnection dbConnection);

        Task<IEnumerable<object>> UpdateAsync(IDbConnection dbConnection);
    }

    public interface IMultipleUpdateBase<TModel> : IUpdateBase<TModel>, IMultipleUpdateBase
        where TModel : class, IModel, new()
    {
        new IEnumerable<TModel> UpdatedModels { get; }

        new IAsyncEnumerable<TModel> UpdateAsSinglyAsync(IDbConnection dbConnection);

        new Task<IEnumerable<TModel>> UpdateAsync(IDbConnection dbConnection);
    }
}