using DbStatute.Interfaces.Builders;
using DbStatute.Interfaces.Fundamentals.Proxies;
using DbStatute.Interfaces.Qualifiers;

namespace DbStatute.Interfaces.Proxies
{
    public interface ISelectProxy : IProxyBase
    {
        IOrderFieldQualifier OrderFieldQualifier { get; }
        ISelectQueryGroupBuilder SelectQueryGroupBuilder { get; }
        IFieldQualifier SelectedFieldQualifier { get; }
    }

    public interface ISelectProxy<TModel> : IProxyBase<TModel>, ISelectProxy
        where TModel : class, IModel, new()
    {
        new IOrderFieldQualifier<TModel> OrderFieldQualifier { get; }
        new ISelectQueryGroupBuilder<TModel> SelectQueryGroupBuilder { get; }
        new IFieldQualifier<TModel> SelectedFieldQualifier { get; }
    }
}