using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Interfaces.Fundamentals.Singles
{
    public interface ISingleMergeBase : IMergeBase
    {
        object MergedModel { get; }

        Task<object> MergeAsync(IDbConnection dbConnection);
    }

    public interface ISingleMergeBase<TModel> : IMergeBase<TModel>, ISingleMergeBase
        where TModel : class, IModel, new()
    {
        new TModel MergedModel { get; }

        new Task<TModel> MergeAsync(IDbConnection dbConnection);
    }
}