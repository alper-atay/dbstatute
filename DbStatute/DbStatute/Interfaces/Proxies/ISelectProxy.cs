using DbStatute.Interfaces.Fundamentals.Proxies;

namespace DbStatute.Interfaces.Proxies
{
    public interface ISelectProxy : ISelectProxyBase
    {
    }

    public interface ISelectProxy<TModel> : ISelectProxyBase<TModel>, ISelectProxy
        where TModel : class, IModel, new()
    {
    }
}