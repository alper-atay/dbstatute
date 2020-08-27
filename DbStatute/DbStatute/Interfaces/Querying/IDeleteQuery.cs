using DbStatute.Interfaces.Querying.Fundamentals;

namespace DbStatute.Interfaces.Querying
{
    public interface IDeleteQuery : IStatuteQueryBase
    {
        ISelectQuery SelectQuery { get; }
    }

    public interface IDeleteQuery<TModel> : IStatuteQueryBase<TModel>, IDeleteQuery
        where TModel : class, IModel, new()
    {
        new ISelectQuery<TModel> SelectQuery { get; }
    }
}