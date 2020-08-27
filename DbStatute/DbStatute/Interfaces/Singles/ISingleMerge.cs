using DbStatute.Interfaces.Fundamentals;
using DbStatute.Interfaces.Querying.Qualifiers;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Interfaces
{
    public interface ISingleMerge : IMergeBase
    {
        object MergedModel { get; }
        IModelQueryQualifier ModelQueryQualifier { get; }

        Task<object> MergeAsync(IDbConnection dbConnection);
    }

    public interface ISingleMerge<TModel> : IMergeBase<TModel>, ISingleMerge
        where TModel : class, IModel, new()
    {
        new TModel MergedModel { get; }
        new IModelQueryQualifier<TModel> ModelQueryQualifier { get; }

        new Task<TModel> MergeAsync(IDbConnection dbConnection);
    }
}