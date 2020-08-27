using DbStatute.Interfaces.Fundamentals.Singles;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleDeleteById : ISingleDeleteBase, IId
    {
    }

    public interface ISingleDeleteById<TModel> : ISingleDeleteBase<TModel>, ISingleDeleteById
        where TModel : class, IModel, new()
    {
    }
}
