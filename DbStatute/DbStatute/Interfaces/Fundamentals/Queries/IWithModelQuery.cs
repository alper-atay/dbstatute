using DbStatute.Interfaces.Queries;

namespace DbStatute.Interfaces.Fundamentals.Queries
{
    public interface IWithModelQuery
    {
        IModelQuery ModelQuery { get; }
    }

    public interface IWithModelQuery<TModel> : IWithModelQuery
        where TModel : class, IModel, new()
    {
        new IModelQuery<TModel> ModelQuery { get; }
    }


}