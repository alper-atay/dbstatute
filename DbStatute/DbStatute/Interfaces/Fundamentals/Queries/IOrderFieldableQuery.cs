using DbStatute.Interfaces.Queries;

namespace DbStatute.Interfaces.Fundamentals.Queries
{
    public interface IOrderFieldableQuery
    {
        IOrderFieldQuery OrderFieldQuery { get; }
    }

    public interface IOrderFieldableQuery<TModel> : IOrderFieldableQuery
        where TModel : class, IModel, new()
    {
        new IOrderFieldQuery<TModel> OrderFieldQuery { get; }
    }
}