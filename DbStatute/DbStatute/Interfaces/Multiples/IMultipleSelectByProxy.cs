using DbStatute.Interfaces.Fundamentals;
using DbStatute.Interfaces.Proxies;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleSelectByProxy<TSelectProxy> : IMultipleSelectBase
        where TSelectProxy : ISelectProxy
    {
        TSelectProxy SelectProxy { get; }
    }

    public interface IMultipleSelectByProxy<TModel, TSelectProxy> : IMultipleSelectBase<TModel>, IMultipleSelectByProxy<TSelectProxy>
        where TModel : class, IModel, new()
        where TSelectProxy : ISelectProxy<TModel>
    {
        new TSelectProxy SelectProxy { get; }
    }
}