namespace DbStatute.Interfaces
{
    public interface IUpdate : IStatute
    {
        int UpdatedCount { get; }
    }

    public interface IUpdate<TModel> : IStatute<TModel>
        where TModel : class, IModel, new()
    {
    }
}