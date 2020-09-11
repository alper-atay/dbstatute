using DbStatute.Interfaces.Fundamentals.Singles;
using DbStatute.Interfaces.Queries;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleSelectById : ISingleSelectBase, IIdentifiableQuery
    {
    }

    public interface ISingleSelectById<TModel> : ISingleSelectBase<TModel>, ISingleSelectById
        where TModel : class, IModel, new()
    {
    }
}