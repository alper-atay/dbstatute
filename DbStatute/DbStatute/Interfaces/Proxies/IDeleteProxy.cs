using DbStatute.Interfaces.Fundamentals.Proxies;

namespace DbStatute.Interfaces.Proxies
{
    public interface IDeleteProxy : IDeleteProxyBase
    {
    }

    public interface IDeleteProxy<TModel> : IDeleteProxyBase<TModel>, IDeleteProxy
        where TModel : class, IModel, new()
    {
    }
}