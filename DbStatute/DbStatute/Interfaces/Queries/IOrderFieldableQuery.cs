namespace DbStatute.Interfaces.Queries
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