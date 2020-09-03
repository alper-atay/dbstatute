using DbStatute.Interfaces.Fundamentals.Proxies;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Interfaces.Qualifiers.Groups;

namespace DbStatute.Interfaces.Proxies
{
    public interface IInsertProxy : IProxyBase
    {
        int? InsertCount { get; set; }

        IFieldQualifier InsertedFieldQualifier { get; }

        IModelQualifierGroup ModelQualifierGroup { get; }
    }

    public interface IInsertProxy<TModel> : IProxyBase<TModel>, IInsertProxy
        where TModel : class, IModel, new()
    {
        new IFieldQualifier<TModel> InsertedFieldQualifier { get; }

        new IModelQualifierGroup<TModel> ModelQualifierGroup { get; }
    }
}