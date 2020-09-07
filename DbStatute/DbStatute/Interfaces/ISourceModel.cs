namespace DbStatute.Interfaces
{
    public interface ISourceModel
    {
        object SourceModel { get; }
    }

    public interface ISourceModel<TModel> : ISourceModel
        where TModel : class, IModel, new()
    {
        new TModel SourceModel { get; }
    }
}