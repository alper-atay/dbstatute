using DbStatute.Interfaces.Fundamentals;
using DbStatute.Interfaces.Proxies;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleDeleteByQuery<TDeleteQuery> : IMultipleDeleteBase
        where TDeleteQuery : IDeleteProxy
    {
        TDeleteQuery DeleteQuery { get; }
    }

    public interface IMultipleDeleteByQuery<TModel, TDeleteQuery> : IMultipleDeleteBase<TModel>, IMultipleDeleteByQuery<TDeleteQuery>
        where TModel : class, IModel, new()
        where TDeleteQuery : IDeleteProxy<TModel>
    {
        new TDeleteQuery DeleteQuery { get; }
    }
}