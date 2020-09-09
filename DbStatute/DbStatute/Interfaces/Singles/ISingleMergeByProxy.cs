using DbStatute.Interfaces.Fundamentals.Singles;
using DbStatute.Interfaces.Proxies;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleMergeByProxy : ISingleMergeBase
    {
        IMergeProxy MergeProxy { get; }
    }

    public interface ISingleMergeByProxy<TModel> : ISingleMergeBase<TModel>, ISingleMergeByProxy
        where TModel : class, IModel, new()
    {
        new IMergeProxy<TModel> MergeProxy { get; }
    }
}