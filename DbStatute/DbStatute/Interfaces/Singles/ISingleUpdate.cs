using DbStatute.Interfaces.Fundamentals.Queries;
using DbStatute.Interfaces.Fundamentals.Singles;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleUpdate : ISingleUpdateBase, ISourceableModelQuery
    {
    }

    public interface ISingleUpdate<TModel> : ISingleUpdateBase<TModel>, ISourceableModelQuery<TModel>, ISingleUpdate
        where TModel : class, IModel, new()
    {
    }
}