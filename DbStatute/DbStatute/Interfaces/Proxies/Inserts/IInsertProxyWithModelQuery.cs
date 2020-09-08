using DbStatute.Interfaces.Fundamentals.Queries;

namespace DbStatute.Interfaces.Proxies.Inserts
{
    public interface IInsertProxyWithModelQuery : IInsertProxy, IWithModelQuery
    {
    }

    public interface IInsertProxyWithModelQuery<TModel> : IInsertProxy<TModel>, IWithModelQuery<TModel>, IInsertProxyWithModelQuery
        where TModel : class, IModel, new()
    {
    }
}