using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Interfaces
{
    public interface IMultipleSelect : ISelect
    {
        IEnumerable<object> SelectedModels { get; }

        IAsyncEnumerable<object> SelectAsSinglyAsync(IDbConnection dbConnection);

        Task<IEnumerable<object>> SelectAsync(IDbConnection dbConnection);

        Task<IEnumerable<object>> SelectByActingAsync(IDbConnection dbConnection, Action<object> action);
    }

    public interface IMultipleSelect<TModel> : ISelect<TModel>, IMultipleSelect
        where TModel : class, IModel, new()
    {
        new IEnumerable<TModel> SelectedModels { get; }

        new IAsyncEnumerable<TModel> SelectAsSinglyAsync(IDbConnection dbConnection);

        new Task<IEnumerable<TModel>> SelectAsync(IDbConnection dbConnection);

        Task<IEnumerable<TModel>> SelectByActingAsync(IDbConnection dbConnection, Action<TModel> action);
    }
}