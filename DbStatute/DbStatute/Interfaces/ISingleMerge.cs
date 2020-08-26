using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Interfaces
{
    public interface ISingleMerge : IMerge
    {
        object MergedModel { get; }
        object RawModel { get; }

        Task<object> MergeAsync(IDbConnection dbConnection);
    }

    public interface ISingleMerge<TModel> : IMerge<TModel>, ISingleMerge
        where TModel : class, IModel, new()
    {
        new TModel MergedModel { get; }
        new TModel RawModel { get; }

        new Task<TModel> MergeAsync(IDbConnection dbConnection);
    }
}