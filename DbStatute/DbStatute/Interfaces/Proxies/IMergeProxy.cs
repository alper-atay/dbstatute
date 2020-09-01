using DbStatute.Interfaces.Builders;
using DbStatute.Interfaces.Fundamentals.Proxies;
using DbStatute.Interfaces.Qualifiers;

namespace DbStatute.Interfaces.Proxies
{
    public interface IMergeProxy : IProxyBase
    {
        IFieldQualifier MergedFieldQualifier { get; }
        IModelBuilder ModelBuilder { get; }
    }

    public interface IMergeProxy<TModel> : IProxyBase<TModel>, IMergeProxy
        where TModel : class, IModel, new()
    {
        new IFieldQualifier<TModel> MergedFieldQualifier { get; }
        new IModelBuilder<TModel> ModelBuilder { get; }
    }
}