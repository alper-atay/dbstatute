using System;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Interfaces
{
    public interface ISingleInsert<TId, TModel> : IInsert
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        TModel InsertedModel { get; }
        TModel RawModel { get; }

        Task<TModel> InsertAsync(IDbConnection dbConnection);
    }
}