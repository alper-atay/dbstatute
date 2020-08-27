using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Interfaces.Fundamentals
{
    public interface IMultipleSelectBase : ISelectBase
    {
        IEnumerable<object> SelectedModels { get; }

        IAsyncEnumerable<object> SelectAsSinglyAsync(IDbConnection dbConnection);

        Task<IEnumerable<object>> SelectAsync(IDbConnection dbConnection);
    }

    public interface IMultipleSelectBase<TModel> : ISelectBase<TModel>, IMultipleSelectBase
        where TModel : class, IModel, new()
    {
        new IEnumerable<TModel> SelectedModels { get; }

        new IAsyncEnumerable<TModel> SelectAsSinglyAsync(IDbConnection dbConnection);

        new Task<IEnumerable<TModel>> SelectAsync(IDbConnection dbConnection);
    }
}