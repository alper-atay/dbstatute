using DbStatute.Interfaces.Querying;
using System;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Interfaces
{
    public interface ISingleUpdateByQuery<TId, TModel, TUpdateQuery> : IUpdateByQuery<TId, TModel, TUpdateQuery>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
        where TUpdateQuery : IUpdateQuery<TId, TModel>
    {
        TModel UpdatedModel { get; }

        Task<TModel> UpdateAsync(IDbConnection dbConnection, TId id);
    }
}