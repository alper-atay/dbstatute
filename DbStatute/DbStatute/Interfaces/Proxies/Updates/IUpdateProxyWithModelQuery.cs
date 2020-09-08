using DbStatute.Interfaces.Fundamentals.Queries;

namespace DbStatute.Interfaces.Proxies.Updates
{
    public interface IUpdateProxyWithModelQuery : IUpdateProxy, IWithModelQuery
    {
    }

    public interface IUpdateProxyWithModelQuery<TModel> : IUpdateProxy<TModel>, IWithModelQuery<TModel>, IUpdateProxyWithModelQuery
        where TModel : class, IModel, new()
    {
    }
}