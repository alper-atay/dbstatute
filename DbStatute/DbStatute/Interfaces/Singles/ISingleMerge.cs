using DbStatute.Interfaces.Fundamentals.Singles;
using DbStatute.Interfaces.Querying;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleMerge : ISingleMergeBase, IRawModel
    {
    }

    public interface ISingleMerge<TModel> : ISingleMergeBase<TModel>, IRawModel<TModel>, ISingleMerge
        where TModel : class, IModel, new()
    {
    }
}