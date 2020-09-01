namespace DbStatute.Interfaces
{
    public interface IReadyModel
    {
        object ReadyModel { get; }
    }

    public interface IReadyModel<TModel> : IReadyModel
        where TModel : class, IModel, new()
    {
        new TModel ReadyModel { get; }
    }
}