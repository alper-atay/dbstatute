using DbStatute.Interfaces.Fundamentals.Queries;
using DbStatute.Interfaces.Fundamentals.Singles;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleUpdate : ISingleUpdateBase, ISourceableQuery
    {
    }

    public interface ISingleUpdate<TModel> : ISingleUpdateBase<TModel>, ISourceableQuery<TModel>, ISingleUpdate
        where TModel : class, IModel, new()
    {
    }
}