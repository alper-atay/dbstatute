using DbStatute.Interfaces.Fundamentals.Proxies;
using DbStatute.Interfaces.Querying.Qualifiers.Fields;

namespace DbStatute.Interfaces.Proxies
{
    public interface ISelectProxy : IStatuteProxyBase
    {
        IFieldQualifier FieldQualifier { get; }
        bool FieldQualifierEnabled { get; set; }
        IOrderFieldQualifier OrderFieldQualifier { get; }
        bool OrderFieldQualifierEnabled { get; set; }
    }

    public interface ISelectProxy<TModel> : IStatuteProxyBase<TModel>, ISelectProxy
        where TModel : class, IModel, new()
    {
        new IFieldQualifier<TModel> FieldQualifier { get; }
        new IOrderFieldQualifier<TModel> OrderFieldQualifier { get; }
    }
}