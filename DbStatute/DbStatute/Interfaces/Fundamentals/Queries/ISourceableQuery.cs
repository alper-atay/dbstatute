namespace DbStatute.Interfaces.Fundamentals.Queries
{
    public interface ISourceableQuery
    {
        object SourceModel { get; }
    }

    public interface ISourceableQuery<TModel> : ISourceableQuery
        where TModel : class, IModel, new()
    {
        new TModel SourceModel { get; }
    }
}