using DbStatute.Interfaces.Querying.Statutes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Interfaces
{
    public interface IMultipleUpdate<TUpdateQuery, TMultipleSelect> : IUpdate
        where TUpdateQuery : IUpdateQuery
        where TMultipleSelect : IMultipleSelect
    {
        TMultipleSelect MultipleSelect { get; }
        TUpdateQuery UpdateQuery { get; }

        IAsyncEnumerable<object> UpdateAsSinglyAsync(IDbConnection dbConnection);

        Task<int> UpdateAsync(IDbConnection dbConnection);

        Task<int> UpdateByActingAsync(IDbConnection dbConnection, Action action);
    }

    public interface IMultipleUpdate<TModel, TUpdateQuery, TMultipleSelect> : IUpdate<TModel>, IMultipleUpdate<TUpdateQuery, TMultipleSelect>
        where TModel : class, IModel, new()
        where TUpdateQuery : IUpdateQuery<TModel>
        where TMultipleSelect : IMultipleSelect<TModel>
    {
        new TMultipleSelect MultipleSelect { get; }
        new TUpdateQuery UpdateQuery { get; }

        new IAsyncEnumerable<TModel> UpdateAsSinglyAsync(IDbConnection dbConnection);

        new Task<int> UpdateAsync(IDbConnection dbConnection);

        Task<int> UpdateByActingAsync(IDbConnection dbConnection, Action<TModel> action);
    }
}