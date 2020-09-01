using DbStatute.Interfaces.Fundamentals.Singles;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleDelete : ISingleDeleteBase, IReadyModel
    {
    }

    public interface ISingleDelete<TModel> : ISingleDeleteBase<TModel>, IReadyModel<TModel>
        where TModel : class, IModel, new()
    {
    }
}