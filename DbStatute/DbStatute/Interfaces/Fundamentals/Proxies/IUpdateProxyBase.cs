using DbStatute.Interfaces.Qualifiers;

namespace DbStatute.Interfaces.Fundamentals.Proxies
{
    public interface IUpdateProxyBase : IProxyBase
    {
        IFieldQualifier UpdatedFieldQualifier { get; }
    }

    public interface IUpdateProxyBase<TModel> : IProxyBase<TModel>, IUpdateProxyBase
    where TModel : class, IModel, new()
    {
        IFieldQualifier<TModel> UpdatedFieldQualifier { get; }
    }
}