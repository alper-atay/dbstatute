using DbStatute.Interfaces.Fundamentals.Multiples;
using DbStatute.Interfaces.Proxies;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleMergeByProxy : IMultipleMergeBase
    {
        IMergeProxy MergeProxy { get; }
    }

    public interface IMultipleMergeByProxy<TModel> : IMultipleMergeBase<TModel>, IMultipleMergeByProxy
        where TModel : class, IModel, new()
    {
        new IMergeProxy<TModel> MergeProxy { get; }
    }
}