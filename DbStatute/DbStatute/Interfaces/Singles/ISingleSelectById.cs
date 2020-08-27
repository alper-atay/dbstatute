using DbStatute.Interfaces.Fundamentals.Singles;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleSelectById : ISingleSelectBase, IId
    {
    }

    public interface ISingleSelectById<TModel> : ISingleSelectBase<TModel>, ISingleSelectById
        where TModel : class, IModel, new()
    {
    }
}
