using DbStatute.Interfaces.Fundamentals.Singles;
using DbStatute.Interfaces.Proxies;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleDeleteByProxy<TDeleteProxy> : ISingleDeleteBase
        where TDeleteProxy : IDeleteProxy
    {
        TDeleteProxy DeleteProxy { get; }
    }

    public interface ISingleDeleteByProxy<TModel, TDeleteProxy> : ISingleDeleteBase<TModel>, ISingleDeleteByProxy<TDeleteProxy>
        where TModel : class, IModel, new()
        where TDeleteProxy : IDeleteProxy<TModel>
    {
        new TDeleteProxy DeleteProxy { get; }
    }
}