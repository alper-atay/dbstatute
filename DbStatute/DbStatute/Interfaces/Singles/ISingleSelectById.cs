namespace DbStatute.Interfaces
{
    public interface ISingleSelectById : ISingleSelect
    {
        object Id { get; }
    }

    public interface ISingleSelectById<TModel> : ISingleSelect<TModel>, ISingleSelectById
        where TModel : class, IModel, new()
    {
    }
}