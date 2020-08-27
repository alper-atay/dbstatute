using DbStatute.Interfaces.Fundamentals.Singles;
using DbStatute.Interfaces.Querying;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleSelectByQuery<TSelectQuery> : ISingleSelectBase
        where TSelectQuery : ISelectQuery
    {
    }

    public interface ISingleSelectByQuery<TModel, TSelectQuery> : ISingleSelectBase<TModel>, ISingleSelectByQuery<TSelectQuery>
        where TModel : class, IModel, new()
        where TSelectQuery : ISelectQuery<TModel>
    {
    }
}