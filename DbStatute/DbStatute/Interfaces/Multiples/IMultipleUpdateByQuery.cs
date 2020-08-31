using DbStatute.Interfaces.Fundamentals.Multiples;
using DbStatute.Interfaces.Proxies;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleUpdateByQuery<TUpdateQuery> : IMultipleUpdateBase
        where TUpdateQuery : IUpdateProxy
    {
        TUpdateQuery UpdateQuery { get; }
    }

    public interface IMultipleUpdateByQuery<TModel, TUpdateQuery> : IMultipleUpdateBase<TModel>, IMultipleUpdateByQuery<TUpdateQuery>
        where TModel : class, IModel, new()
        where TUpdateQuery : IUpdateProxy<TModel>
    {
        new TUpdateQuery UpdateQuery { get; }
    }
}