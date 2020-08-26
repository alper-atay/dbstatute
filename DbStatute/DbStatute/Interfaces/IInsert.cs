namespace DbStatute.Interfaces
{
    public interface IInsert : IStatute
    {
        int InsertedCount { get; }
    }

    public interface IInsert<TModel> : IStatute<TModel>, IInsert
        where TModel : class, IModel, new()
    {
    }
}