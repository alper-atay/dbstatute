using DbStatute.Interfaces.Fundamentals.Queries;
using DbStatute.Interfaces.Qualifiers;

namespace DbStatute.Interfaces.Fundamentals.Proxies
{
    public interface IUpdateProxyBase : IProxyBase, IFieldableQuery
    {
    }

    public interface IUpdateProxyBase<TModel> : IProxyBase<TModel>, IFieldableQuery<TModel>, IUpdateProxyBase
    where TModel : class, IModel, new()
    {

    }
}