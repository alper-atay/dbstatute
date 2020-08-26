using DbStatute.Interfaces.Querying.Statutes;

namespace DbStatute.Interfaces
{
    public interface ISingleSelectByQuery<TSelectQuery> : ISingleSelect
        where TSelectQuery : ISelectQuery
    {
        TSelectQuery SelectQuery { get; }
    }

    public interface ISingleSelectByQuery<TModel, TSelectQuery> : ISingleSelect<TModel>, ISingleSelectByQuery<TSelectQuery>
        where TModel : class, IModel, new()
        where TSelectQuery : ISelectQuery<TModel>
    {
        new TSelectQuery SelectQuery { get; }
    }
}