using System;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Interfaces
{
    public interface ISingleMerge<TId, TModel> : IMerge
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
    {
        TModel MergedModel { get; }
        TModel RawModel { get; }

        Task<TModel> MergeAsync(IDbConnection dbConnection);
    }
}