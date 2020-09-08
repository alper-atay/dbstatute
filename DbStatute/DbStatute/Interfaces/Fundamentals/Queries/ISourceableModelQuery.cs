namespace DbStatute.Interfaces.Fundamentals.Queries
{
    public interface ISourceableModelQuery
    {
        object SourceModel { get; }
    }

    public interface ISourceableModelQuery<TModel> : ISourceableModelQuery
        where TModel : class, IModel, new()
    {
        new TModel SourceModel { get; }
    }
}