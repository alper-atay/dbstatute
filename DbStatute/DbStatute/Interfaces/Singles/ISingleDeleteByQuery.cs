using DbStatute.Interfaces.Fundamentals.Singles;
using DbStatute.Interfaces.Querying;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleDeleteByQuery<TDeleteQuery> : ISingleDeleteBase
        where TDeleteQuery : IDeleteQuery
    {
    }

    public interface ISingleDeleteByQuery<TModel, TDeleteQuery> : ISingleDeleteBase<TModel>, ISingleDeleteByQuery<TDeleteQuery>
        where TModel : class, IModel, new()
        where TDeleteQuery : IDeleteQuery<TModel>
    {
    }
}