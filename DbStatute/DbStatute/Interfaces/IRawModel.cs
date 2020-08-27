namespace DbStatute.Interfaces
{
    public interface IRawModel
    {
        object RawModel { get; }
    }

    public interface IRawModel<TModel> : IRawModel
        where TModel : class, IModel, new()
    {
        new TModel RawModel { get; }
    }
}