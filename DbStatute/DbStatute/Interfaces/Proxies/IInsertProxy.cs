using DbStatute.Interfaces.Builders;
using DbStatute.Interfaces.Fundamentals.Proxies;
using DbStatute.Interfaces.Qualifiers;

namespace DbStatute.Interfaces.Proxies
{
    public interface IInsertProxy : IProxyBase
    {
        IFieldQualifier InsertedFieldQualifier { get; }

        IModelBuilder ModelBuilder { get; }
    }

    public interface IInsertProxy<TModel> : IProxyBase<TModel>, IInsertProxy
        where TModel : class, IModel, new()
    {
        new IFieldQualifier<TModel> InsertedFieldQualifier { get; }

        new IModelBuilder<TModel> ModelBuilder { get; }
    }
}