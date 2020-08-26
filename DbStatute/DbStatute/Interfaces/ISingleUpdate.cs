using DbStatute.Interfaces.Querying.Statutes;
using System;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Interfaces
{
    public interface ISingleUpdate<TId, TModel, TUpdateQuery, TSingleSelect> : IUpdate
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
        where TUpdateQuery : IUpdateQuery<TId, TModel>
        where TSingleSelect : ISingleSelect<TId, TModel>
    {
        TSingleSelect SingleSelect { get; }
        TModel UpdatedModel { get; }
        TUpdateQuery UpdateQuery { get; }

        Task<TModel> UpdateAsync(IDbConnection dbConnection);
    }
}