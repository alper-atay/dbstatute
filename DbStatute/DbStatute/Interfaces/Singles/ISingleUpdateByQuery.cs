using DbStatute.Interfaces.Fundamentals.Singles;
using DbStatute.Interfaces.Proxies;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleUpdateByQuery<TUpdateQuery> : ISingleUpdateBase
        where TUpdateQuery : IUpdateProxy
    {
        TUpdateQuery UpdateQuery { get; }
    }

    public interface ISingleUpdateByQuery<TModel, TUpdateQuery> : ISingleUpdateBase<TModel>, ISingleUpdateByQuery<TUpdateQuery>
        where TModel : class, IModel, new()
        where TUpdateQuery : IUpdateProxy<TModel>
    {
        new TUpdateQuery UpdateQuery { get; }
    }
}