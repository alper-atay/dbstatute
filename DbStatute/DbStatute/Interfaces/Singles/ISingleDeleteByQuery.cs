using DbStatute.Interfaces.Fundamentals.Singles;
using DbStatute.Interfaces.Proxies;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleDeleteByQuery<TDeleteQuery> : ISingleDeleteBase
        where TDeleteQuery : IDeleteProxy
    {
        TDeleteQuery DeleteQuery { get; }
    }

    public interface ISingleDeleteByQuery<TModel, TDeleteQuery> : ISingleDeleteBase<TModel>, ISingleDeleteByQuery<TDeleteQuery>
        where TModel : class, IModel, new()
        where TDeleteQuery : IDeleteProxy<TModel>
    {
        new TDeleteQuery DeleteQuery { get; }
    }
}