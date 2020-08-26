using DbStatute.Interfaces.Querying;

namespace DbStatute.Interfaces
{
    public interface IMultipleSelectByQuery<TSelectQuery> : IMultipleSelect
        where TSelectQuery : ISelectQuery
    {
        TSelectQuery SelectQuery { get; }
    }

    public interface IMultipleSelectByQuery<TModel, TSelectQuery> : IMultipleSelect<TModel>, IMultipleSelectByQuery<TSelectQuery>
        where TModel : class, IModel, new()
        where TSelectQuery : ISelectQuery<TModel>
    {
        new TSelectQuery SelectQuery { get; }
    }
}