using DbStatute.Interfaces.Querying;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Interfaces
{
    public interface IMultipleUpdateByQuery<TId, TModel, TUpdateQuery>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
        where TUpdateQuery : IUpdateQuery<TId, TModel>
    {
        TUpdateQuery UpdateQuery { get; }

        Task<IEnumerable<TId>> UpdateAsSingularAsync(IDbConnection dbConnection, IEnumerable<TId> ids);

        Task<IEnumerable<TId>> UpdateAsSingularEmissionAsync(IDbConnection dbConnection, IEnumerable<TId> ids, Action<TModel> action);
    }
}