using DbStatute.Interfaces.Fundamentals.Singles;
using DbStatute.Interfaces.Querying;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleMergeByRawModel : ISingleMergeBase, IRawModel
    {
    }

    public interface ISingleMergeByRawModel<TModel> : ISingleMergeBase<TModel>, IRawModel<TModel>, ISingleMergeByRawModel
        where TModel : class, IModel, new()
    {
    }
}