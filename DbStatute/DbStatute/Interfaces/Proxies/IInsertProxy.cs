using DbStatute.Interfaces.Fundamentals.Proxies;
using DbStatute.Interfaces.Querying.Builders;
using DbStatute.Interfaces.Querying.Qualifiers.Fields;

namespace DbStatute.Interfaces.Proxies
{
    public interface IInsertProxy : IStatuteProxyBase
    {
        IModelBuilder ModelBuilder { get; }
        IFieldQualifier InsertedFieldQualifier { get; }
    }

    public interface IInsertProxy<TModel> : IStatuteProxyBase<TModel>, IInsertProxy
        where TModel : class, IModel, new()
    {
        new IModelBuilder<TModel> ModelBuilder { get; }
        new IFieldQualifier<TModel> InsertedFieldQualifier { get; }
    }
}