using DbStatute.Interfaces.Queries;

namespace DbStatute.Interfaces.Fundamentals.Queries
{
    public interface IModelableQuery
    {
        IModelQuery ModelQuery { get; }
    }

    public interface IModelableQuery<TModel> : IModelableQuery
        where TModel : class, IModel, new()
    {
        new IModelQuery<TModel> ModelQuery { get; }
    }


}