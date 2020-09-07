using DbStatute.Interfaces.Fundamentals.Singles;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleUpdate : ISingleUpdateBase, ISourceModel
    {
    }

    public interface ISingleUpdate<TModel> : ISingleUpdateBase<TModel>, ISourceModel<TModel>, ISingleUpdate
        where TModel : class, IModel, new()
    {
    }
}