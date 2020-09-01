using DbStatute.Interfaces.Fundamentals.Singles;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleUpdate : ISingleUpdateBase, IReadyModel
    {
    }

    public interface ISingleUpdate<TModel> : ISingleUpdateBase<TModel>, IReadyModel<TModel>, ISingleUpdate
        where TModel : class, IModel, new()
    {
    }
}