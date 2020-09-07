using DbStatute.Interfaces.Fundamentals.Proxies;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Interfaces.Queries;

namespace DbStatute.Interfaces.Proxies
{
    public interface IMergeProxy : IMergeProxyBase
    {
        IFieldQualifier MergedFieldQualifier { get; }

        IModelQuery ModelQuery { get; }
    }

    public interface IMergeProxy<TModel> : IMergeProxyBase<TModel>, IMergeProxy
        where TModel : class, IModel, new()
    {
        new IFieldQualifier<TModel> MergedFieldQualifier { get; }

        new IModelQuery<TModel> ModelQuery { get; }
    }
}