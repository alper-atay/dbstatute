using DbStatute.Interfaces.Queries;

namespace DbStatute.Interfaces.Fundamentals.Queries
{
    public interface IWithWhereQuery
    {
        IWhereQuery WhereQuery { get; }
    }

    public interface IWithWhereQuery<TModel> : IWithWhereQuery
        where TModel : class, IModel, new()
    {
        new IWhereQuery<TModel> WhereQuery { get; }
    }
}