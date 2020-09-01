using DbStatute.Interfaces.Fundamentals.Multiples;
using DbStatute.Interfaces.Proxies;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleUpdateByProxy<TUpdateProxy> : IMultipleUpdateBase
        where TUpdateProxy : IUpdateProxy
    {
        TUpdateProxy UpdateProxy { get; }
    }

    public interface IMultipleUpdateByQuery<TModel, TUpdateQuery> : IMultipleUpdateBase<TModel>, IMultipleUpdateByProxy<TUpdateQuery>
        where TModel : class, IModel, new()
        where TUpdateQuery : IUpdateProxy<TModel>
    {
        new TUpdateQuery UpdateProxy { get; }
    }
}