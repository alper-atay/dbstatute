using DbStatute.Interfaces.Fundamentals.Multiples;
using DbStatute.Interfaces.Proxies;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleSelectByProxy : IMultipleSelectBase
    {
        ISelectProxy SelectProxy { get; }
    }

    public interface IMultipleSelectByProxy<TModel> : IMultipleSelectBase<TModel>, IMultipleSelectByProxy
        where TModel : class, IModel, new()
    {
        new ISelectProxy<TModel> SelectProxy { get; }
    }
}