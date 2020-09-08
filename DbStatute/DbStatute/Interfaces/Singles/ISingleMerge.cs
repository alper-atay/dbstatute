using DbStatute.Interfaces.Fundamentals.Queries;
using DbStatute.Interfaces.Fundamentals.Singles;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleMerge : ISingleMergeBase, ISourceableModelQuery
    {
    }

    public interface ISingleMerge<TModel> : ISingleMergeBase<TModel>, ISourceableModelQuery<TModel>, ISingleMerge
        where TModel : class, IModel, new()
    {
    }
}