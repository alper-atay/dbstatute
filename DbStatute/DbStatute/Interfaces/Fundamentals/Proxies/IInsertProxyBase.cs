using DbStatute.Interfaces.Fundamentals.Queries;

namespace DbStatute.Interfaces.Fundamentals.Proxies
{
    public interface IInsertProxyBase : IProxyBase, IFieldableQuery
    {
    }

    public interface IInsertProxyBase<TModel> : IProxyBase<TModel>, IFieldableQuery<TModel>, IInsertProxyBase
        where TModel : class, IModel, new()
    {
    }
}