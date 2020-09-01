using DbStatute.Interfaces.Fundamentals.Singles;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleMerge : ISingleMergeBase, IReadyModel
    {
    }

    public interface ISingleMerge<TModel> : ISingleMergeBase<TModel>, IReadyModel<TModel>, ISingleMerge
        where TModel : class, IModel, new()
    {
    }
}