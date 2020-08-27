using DbStatute.Interfaces.Fundamentals;
using DbStatute.Interfaces.Querying;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleDeleteByQuery<TDeleteQuery> : IMultipleDeleteBase
        where TDeleteQuery : IDeleteQuery
    {
        TDeleteQuery DeleteQuery { get; }
    }

    public interface IMultipleDeleteByQuery<TModel, TDeleteQuery> : IMultipleDeleteBase<TModel>, IMultipleDeleteByQuery<TDeleteQuery>
        where TModel : class, IModel, new()
        where TDeleteQuery : IDeleteQuery<TModel>
    {
        new TDeleteQuery DeleteQuery { get; }
    }
}