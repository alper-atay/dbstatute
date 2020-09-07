using DbStatute.Interfaces.Fundamentals.Singles;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleMerge : ISingleMergeBase, ISourceModel
    {
    }

    public interface ISingleMerge<TModel> : ISingleMergeBase<TModel>, ISourceModel<TModel>, ISingleMerge
        where TModel : class, IModel, new()
    {
    }
}