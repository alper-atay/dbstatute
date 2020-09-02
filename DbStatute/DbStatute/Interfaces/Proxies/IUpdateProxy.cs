using DbStatute.Interfaces.Fundamentals.Proxies;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Interfaces.Qualifiers.Groups;

namespace DbStatute.Interfaces.Proxies
{
    public interface IUpdateProxy : IProxyBase
    {
        IFieldQualifier MergedFieldQualifier { get; }

        IModelQualifierGroup ModelQualifierGroup { get; }
    }

    public interface IUpdateProxy<TModel> : IProxyBase<TModel>, IUpdateProxy
        where TModel : class, IModel, new()
    {
        new IFieldQualifier<TModel> MergedFieldQualifier { get; }

        new IModelQualifierGroup<TModel> ModelQualifierGroup { get; }
    }
}