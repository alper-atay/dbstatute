using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Interfaces
{
    public interface IMultipleSelect<TId, TModel> : ISelect
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        IEnumerable<TModel> SelectedModels { get; }

        IAsyncEnumerable<TModel> SelectAsSinglyAsync(IDbConnection dbConnection);

        Task<IEnumerable<TModel>> SelectAsync(IDbConnection dbConnection);

        Task<IEnumerable<TModel>> SelectByActingAsync(IDbConnection dbConnection, Action<TModel> action);
    }
}