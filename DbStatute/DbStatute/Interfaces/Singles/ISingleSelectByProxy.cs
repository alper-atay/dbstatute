using DbStatute.Interfaces.Fundamentals.Singles;
using DbStatute.Interfaces.Proxies;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleSelectByProxy<TSelectProxy> : ISingleSelectBase
        where TSelectProxy : ISelectProxy
    {
        TSelectProxy SelectProxy { get; }
    }

    public interface ISingleSelectByProxy<TModel, TSelectProxy> : ISingleSelectBase<TModel>, ISingleSelectByProxy<TSelectProxy>
        where TModel : class, IModel, new()
        where TSelectProxy : ISelectProxy<TModel>
    {
        new TSelectProxy SelectProxy { get; }
    }
}