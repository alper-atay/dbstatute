using DbStatute.Interfaces.Fundamentals.Proxies;
using DbStatute.Interfaces.Qualifiers;

namespace DbStatute.Interfaces.Proxies
{
    public interface IUpdateProxyOnRawModel : IProxyBase
    {
        IFieldQualifier FieldQualifier { get; }

        IPredicateFieldQualifier PredicateFieldQualifier { get; }
    }

    public interface IUpdateProxyOnRawModel<TModel> : IUpdateProxy<TModel>, IUpdateProxyOnRawModel
        where TModel : class, IModel, new()
    {
        new IFieldQualifier<TModel> FieldQualifier { get; }

        new IPredicateFieldQualifier<TModel> PredicateFieldQualifier { get; }
    }
}