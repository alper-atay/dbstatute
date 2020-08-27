using DbStatute.Interfaces.Fundamentals.Singles;
using DbStatute.Interfaces.Querying;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleUpdateByQuery<TUpdateQuery> : ISingleUpdateBase
        where TUpdateQuery : IUpdateQuery
    {
        TUpdateQuery UpdateQuery { get; }
    }

    public interface ISingleUpdateByQuery<TModel, TUpdateQuery> : ISingleUpdateBase<TModel>, ISingleUpdateByQuery<TUpdateQuery>
        where TModel : class, IModel, new()
        where TUpdateQuery : IUpdateQuery<TModel>
    {
        new TUpdateQuery UpdateQuery { get; }
    }
}