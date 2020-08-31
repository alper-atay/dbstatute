using DbStatute.Interfaces.Fundamentals.Singles;
using DbStatute.Interfaces.Proxies;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleSelectByQuery<TSelectQuery> : ISingleSelectBase
        where TSelectQuery : ISelectProxy
    {
    }

    public interface ISingleSelectByQuery<TModel, TSelectQuery> : ISingleSelectBase<TModel>, ISingleSelectByQuery<TSelectQuery>
        where TModel : class, IModel, new()
        where TSelectQuery : ISelectProxy<TModel>
    {
    }
}