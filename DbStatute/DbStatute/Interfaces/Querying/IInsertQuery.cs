using DbStatute.Interfaces.Querying.Qualifiers;

namespace DbStatute.Interfaces.Querying
{
    public interface IInsertQuery : IStatuteQuery
    {
        IFieldQualifier FieldQualifier { get; }
    }

    public interface IInsertQuery<TModel> : IStatuteQuery<TModel>, IInsertQuery
        where TModel : class, IModel, new()
    {
        new IFieldQualifier<TModel> FieldQualifier { get; }
    }
}