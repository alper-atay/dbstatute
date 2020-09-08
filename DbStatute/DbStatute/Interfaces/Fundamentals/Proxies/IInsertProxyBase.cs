using DbStatute.Interfaces.Qualifiers;

namespace DbStatute.Interfaces.Fundamentals.Proxies
{
    public interface IInsertProxyBase : IProxyBase
    {
        IFieldQualifier InsertedFieldQualifier { get; }
    }

    public interface IInsertProxyBase<TModel> : IProxyBase<TModel>, IInsertProxyBase
        where TModel : class, IModel, new()
    {
        new IFieldQualifier<TModel> InsertedFieldQualifier { get; }
    }
}