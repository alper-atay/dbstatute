using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Interfaces.Fundamentals.Multiples
{
    public interface IMultipleMergeBase : IMergeBase
    {
        IEnumerable<object> MergedModels { get; }

        IAsyncEnumerable<object> MergeAsSinglyAsync(IDbConnection dbConnection);

        Task<IEnumerable<object>> MergeAsync(IDbConnection dbConnection);
    }

    public interface IMultipleMergeBase<TModel> : IMergeBase<TModel>, IMultipleMergeBase
        where TModel : class, IModel, new()
    {
        new IEnumerable<TModel> MergedModels { get; }

        new IAsyncEnumerable<TModel> MergeAsSinglyAsync(IDbConnection dbConnection);

        new Task<IEnumerable<TModel>> MergeAsync(IDbConnection dbConnection);
    }
}