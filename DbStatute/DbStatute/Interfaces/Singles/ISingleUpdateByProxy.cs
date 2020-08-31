using DbStatute.Interfaces.Fundamentals.Singles;
using DbStatute.Interfaces.Proxies;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleUpdateByProxy<TUpdateProxy> : ISingleUpdateBase
        where TUpdateProxy : IUpdateProxy
    {
        TUpdateProxy UpdateProxy { get; }
    }

    public interface ISingleUpdateByProxy<TModel, TUpdateProxy> : ISingleUpdateBase<TModel>, ISingleUpdateByProxy<TUpdateProxy>
        where TModel : class, IModel, new()
        where TUpdateProxy : IUpdateProxy<TModel>
    {
        new TUpdateProxy UpdateProxy { get; }
    }
}