using DbStatute.Interfaces.Fundamentals.Proxies;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Interfaces.Queries;

namespace DbStatute.Interfaces.Proxies
{
    public interface IUpdateProxy : IUpdateProxyBase
    {
        IPredicateFieldQualifier PredicateFieldQualifier { get; }

        IFieldQualifier UpdatedFieldQualifier { get; }

        IWhereQuery WhereQuery { get; }
    }

    public interface IUpdateProxy<TModel> : IUpdateProxyBase<TModel>, IUpdateProxy
        where TModel : class, IModel, new()
    {
        new IPredicateFieldQualifier<TModel> PredicateFieldQualifier { get; }

        new IFieldQualifier<TModel> UpdatedFieldQualifier { get; }

        new IWhereQuery<TModel> WhereQuery { get; }
    }
}