namespace DbStatute.Interfaces
{
    public interface IDelete : IStatute
    {
        int DeletedCount { get; }
    }

    public interface IDelete<TModel> : IStatute<TModel>, IDelete
        where TModel : class, IModel, new()
    {
    }
}