using DbStatute.Interfaces.Fundamentals.Queries;

namespace DbStatute.Interfaces.Proxies.Updates
{
    public interface IUpdateProxyWithWhereQuery : IUpdateProxy, IWithWhereQuery
    {
    }

    public interface IUpdateProxyWithWhereQuery<TModel> : IUpdateProxy<TModel>, IWithModelQuery<TModel>, IUpdateProxyWithWhereQuery
        where TModel : class, IModel, new()
    {
    }
}