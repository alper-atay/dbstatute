using DbStatute.Interfaces.Fundamentals;
using DbStatute.Interfaces.Proxies;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleDeleteByProxy<TDeleteProxy> : IMultipleDeleteBase
        where TDeleteProxy : IDeleteProxy
    {
        TDeleteProxy DeleteProxy { get; }
    }

    public interface IMultipleDeleteByProxy<TModel, TDeleteProxy> : IMultipleDeleteBase<TModel>, IMultipleDeleteByProxy<TDeleteProxy>
        where TModel : class, IModel, new()
        where TDeleteProxy : IDeleteProxy<TModel>
    {
        new TDeleteProxy DeleteProxy { get; }
    }
}