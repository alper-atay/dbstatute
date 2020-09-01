using DbStatute.Interfaces.Builders;
using DbStatute.Interfaces.Fundamentals.Proxies;
using DbStatute.Interfaces.Qualifiers;

namespace DbStatute.Interfaces.Proxies
{
    public interface IInsertProxy : IProxyBase
    {
        IModelBuilder ModelBuilder { get; }
        IFieldQualifier InsertedFieldQualifier { get; }
    }

    public interface IInsertProxy<TModel> : IProxyBase<TModel>, IInsertProxy
        where TModel : class, IModel, new()
    {
        new IModelBuilder<TModel> ModelBuilder { get; }
        new IFieldQualifier<TModel> InsertedFieldQualifier { get; }
    }
}