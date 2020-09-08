using DbStatute.Interfaces.Fundamentals.Queries;
using DbStatute.Interfaces.Proxies;

namespace DbStatute.Interfaces.Fundamentals.Proxies
{
    public interface IDeleteProxyBase : IProxyBase, ISearchableQuery
    {
    }

    public interface IDeleteProxyBase<TModel> : IProxyBase<TModel>, ISearchableQuery<TModel>, IDeleteProxyBase
        where TModel : class, IModel, new()
    {
    }
}