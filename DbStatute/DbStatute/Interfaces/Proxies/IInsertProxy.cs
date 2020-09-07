using DbStatute.Interfaces.Fundamentals.Proxies;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Interfaces.Queries;

namespace DbStatute.Interfaces.Proxies
{
    public interface IInsertProxy : IProxyBase
    {
        IFieldQualifier InsertedFieldQualifier { get; }

        IModelQuery ModelQuery { get; }
    }

    public interface IInsertProxy<TModel> : IProxyBase<TModel>, IInsertProxy
        where TModel : class, IModel, new()
    {
        new IFieldQualifier<TModel> InsertedFieldQualifier { get; }

        new IModelQuery<TModel> ModelQuery { get; }
    }
}