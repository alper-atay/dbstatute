using DbStatute.Interfaces.Querying.Statutes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Interfaces
{
    public interface IMultipleUpdate<TId, TModel, TUpdateQuery, TMultipleSelect> : IUpdate
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
        where TUpdateQuery : IUpdateQuery<TId, TModel>
        where TMultipleSelect : IMultipleSelect<TId, TModel>
    {
        TMultipleSelect MultipleSelect { get; }
        TUpdateQuery UpdateQuery { get; }

        IAsyncEnumerable<TModel> UpdateAsSinglyAsync(IDbConnection dbConnection);

        Task<int> UpdateAsync(IDbConnection dbConnection);

        Task<int> UpdateByActingAsync(IDbConnection dbConnection, Action<TModel> action);
    }
}