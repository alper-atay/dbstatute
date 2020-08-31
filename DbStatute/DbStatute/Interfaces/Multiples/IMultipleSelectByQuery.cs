using DbStatute.Interfaces.Fundamentals;
using DbStatute.Interfaces.Proxies;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleSelectByQuery<TSelectQuery> : IMultipleSelectBase
        where TSelectQuery : ISelectProxy
    {
        TSelectQuery SelectQuery { get; }
    }

    public interface IMultipleSelectByQuery<TModel, TSelectQuery> : IMultipleSelectBase<TModel>, IMultipleSelectByQuery<TSelectQuery>
        where TModel : class, IModel, new()
        where TSelectQuery : ISelectProxy<TModel>
    {
        new TSelectQuery SelectQuery { get; }
    }
}