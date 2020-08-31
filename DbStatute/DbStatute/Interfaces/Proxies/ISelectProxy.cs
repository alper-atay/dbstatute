using DbStatute.Interfaces.Fundamentals.Proxies;
using DbStatute.Interfaces.Querying.Builders;
using DbStatute.Interfaces.Querying.Qualifiers.Fields;

namespace DbStatute.Interfaces.Proxies
{
    public interface ISelectProxy : IStatuteProxyBase
    {
        IOrderFieldQualifier OrderFieldQualifier { get; }
        bool OrderFieldQualifierEnabled { get; set; }
        IQueryGroupBuilder QueryGroupBuilder { get; }
        IFieldQualifier SelectedFieldQualifier { get; }
        bool SelectedFieldQualifierEnabled { get; set; }
    }

    public interface ISelectProxy<TModel> : IStatuteProxyBase<TModel>, ISelectProxy
        where TModel : class, IModel, new()
    {
        new IOrderFieldQualifier<TModel> OrderFieldQualifier { get; }
        new IQueryGroupBuilder<TModel> QueryGroupBuilder { get; }
        new IFieldQualifier<TModel> SelectedFieldQualifier { get; }
    }
}