using DbStatute.Interfaces.Fundamentals.Proxies;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Interfaces.Qualifiers.Groups;

namespace DbStatute.Interfaces.Proxies
{
    public interface IMergeProxy : IProxyBase
    {
        IFieldQualifier MergedFieldQualifier { get; }

        IModelQualifierGroup ModelQualifierGroup { get; }
    }

    public interface IMergeProxy<TModel> : IProxyBase<TModel>, IMergeProxy
        where TModel : class, IModel, new()
    {
        new IFieldQualifier<TModel> MergedFieldQualifier { get; }

        new IModelQualifierGroup<TModel> ModelQualifierGroup { get; }
    }
}