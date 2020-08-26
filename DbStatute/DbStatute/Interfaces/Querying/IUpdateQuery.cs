using DbStatute.Interfaces.Querying.Qualifiers;

namespace DbStatute.Interfaces.Querying
{
    public interface IUpdateQuery : IStatuteQuery
    {
        IFieldQualifier FieldQualifier { get; }
    }

    public interface IUpdateQuery<TModel> : IStatuteQuery<TModel>, IUpdateQuery
        where TModel : class, IModel, new()
    {
        new IFieldQualifier<TModel> FieldQualifier { get; }
    }
}