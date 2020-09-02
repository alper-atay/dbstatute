using DbStatute.Interfaces.Fundamentals.Proxies;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Interfaces.Qualifiers.Groups;

namespace DbStatute.Interfaces.Proxies
{
    public interface ISelectProxy : IProxyBase
    {
        IOrderFieldQualifier OrderFieldQualifier { get; }

        IFieldQualifier SelectedFieldQualifier { get; }

        ISelectQualifierGroup SelectQualifierGroup { get; }
    }

    public interface ISelectProxy<TModel> : IProxyBase<TModel>, ISelectProxy
        where TModel : class, IModel, new()
    {
        new IOrderFieldQualifier<TModel> OrderFieldQualifier { get; }

        new IFieldQualifier<TModel> SelectedFieldQualifier { get; }

        new ISelectQualifierGroup<TModel> SelectQualifierGroup { get; }
    }
}