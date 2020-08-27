using DbStatute.Interfaces.Fundamentals;
using DbStatute.Interfaces.Querying;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleSelectByQuery<TSelectQuery> : IMultipleSelectBase
        where TSelectQuery : ISelectQuery
    {
        TSelectQuery SelectQuery { get; }
    }

    public interface IMultipleSelectByQuery<TModel, TSelectQuery> : IMultipleSelectBase<TModel>, IMultipleSelectByQuery<TSelectQuery>
        where TModel : class, IModel, new()
        where TSelectQuery : ISelectQuery<TModel>
    {
        new TSelectQuery SelectQuery { get; }
    }
}