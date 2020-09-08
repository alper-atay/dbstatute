using DbStatute.Interfaces.Fundamentals.Queries;
using DbStatute.Interfaces.Fundamentals.Singles;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleDeleteById : ISingleDeleteBase, IIdentifiableQuery
    {
    }

    public interface ISingleDeleteById<TModel> : ISingleDeleteBase<TModel>, ISingleDeleteById
        where TModel : class, IModel, new()
    {
    }
}