using DbStatute.Interfaces.Fundamentals.Multiples;
using DbStatute.Interfaces.Querying;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleUpdateByQuery<TUpdateQuery> : IMultipleUpdateBase
        where TUpdateQuery : IUpdateQuery
    {
        TUpdateQuery UpdateQuery { get; }
    }

    public interface IMultipleUpdateByQuery<TModel, TUpdateQuery> : IMultipleUpdateBase<TModel>, IMultipleUpdateByQuery<TUpdateQuery>
        where TModel : class, IModel, new()
        where TUpdateQuery : IUpdateQuery<TModel>
    {
        new TUpdateQuery UpdateQuery { get; }
    }
}