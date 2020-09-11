using DbStatute.Interfaces.Fundamentals.Singles;
using DbStatute.Interfaces.Queries;

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