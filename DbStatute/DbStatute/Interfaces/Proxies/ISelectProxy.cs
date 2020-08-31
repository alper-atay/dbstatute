using DbStatute.Interfaces.Fundamentals.Proxies;
using DbStatute.Interfaces.Querying.Builders;
using DbStatute.Interfaces.Querying.Qualifiers.Fields;

namespace DbStatute.Interfaces.Proxies
{
    public interface ISelectProxy : IStatuteProxyBase
    {
        IOrderFieldQualifier OrderFieldQualifier { get; }
        ISelectQueryGroupBuilder SelectQueryGroupBuilder { get; }
        IFieldQualifier SelectedFieldQualifier { get; }
    }

    public interface ISelectProxy<TModel> : IStatuteProxyBase<TModel>, ISelectProxy
        where TModel : class, IModel, new()
    {
        new IOrderFieldQualifier<TModel> OrderFieldQualifier { get; }
        new ISelectQueryGroupBuilder<TModel> SelectQueryGroupBuilder { get; }
        new IFieldQualifier<TModel> SelectedFieldQualifier { get; }
    }
}