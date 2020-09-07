using DbStatute.Interfaces.Fundamentals.Proxies;

namespace DbStatute.Interfaces.Proxies
{
    public interface IUpdateProxy : IUpdateProxyBase
    {
    }

    public interface IUpdateProxy<TModel> : IUpdateProxyBase<TModel>, IUpdateProxy
        where TModel : class, IModel, new()
    {
    }
}