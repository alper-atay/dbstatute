using DbStatute.Interfaces.Fundamentals.Singles;
using DbStatute.Interfaces.Proxies;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleMergeByProxy<TMergeProxy> : ISingleMergeBase
        where TMergeProxy : IMergeProxy
    {
        TMergeProxy MergeProxy { get; }
    }

    public interface ISingleMergeByProxy<TModel, TMergeProxy> : ISingleMergeBase<TModel>, ISingleMergeByProxy<TMergeProxy>
        where TModel : class, IModel, new()
        where TMergeProxy : IMergeProxy<TModel>
    {
        new TMergeProxy MergeProxy { get; }
    }
}