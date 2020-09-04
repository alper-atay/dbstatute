using DbStatute.Interfaces.Fundamentals.Proxies;
using DbStatute.Interfaces.Qualifiers;

namespace DbStatute.Interfaces.Proxies
{
    public interface IUpdateProxy : IProxyBase
    {
        IFieldQualifier FieldQualifier { get; }

        IPredicateFieldQualifier PredicateFieldQualifier { get; }

        IValueFieldQualifier ValueFieldQualifier { get; }
    }

    public interface IUpdateProxy<TModel> : IProxyBase<TModel>, IUpdateProxy
        where TModel : class, IModel, new()
    {
        new IFieldQualifier<TModel> FieldQualifier { get; }

        new IPredicateFieldQualifier<TModel> PredicateFieldQualifier { get; }

        new IValueFieldQualifier<TModel> ValueFieldQualifier { get; }
    }
}