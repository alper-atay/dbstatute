namespace DbStatute.Interfaces.Querying.Statutes
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