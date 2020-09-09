using DbStatute.Interfaces.Fundamentals.Queries;
using DbStatute.Interfaces.Fundamentals.Singles;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleMerge : ISingleMergeBase, ISourceableQuery
    {
    }

    public interface ISingleMerge<TModel> : ISingleMergeBase<TModel>, ISourceableQuery<TModel>, ISingleMerge
        where TModel : class, IModel, new()
    {
    }
}