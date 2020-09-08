using DbStatute.Interfaces.Queries;

namespace DbStatute.Interfaces.Fundamentals.Queries
{
    public interface IFieldableQuery
    {
        IFieldQuery FieldQuery { get; }
    }

    public interface IFieldableQuery<TModel> : IFieldableQuery
        where TModel : class, IModel, new()
    {
        new IFieldQuery<TModel> FieldQuery { get; }
    }
}