using DbStatute.Interfaces.Fundamentals.Queries;
using DbStatute.Interfaces.Fundamentals.Singles;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleDelete : ISingleDeleteBase, ISourceableQuery
    {
    }

    public interface ISingleDelete<TModel> : ISingleDeleteBase<TModel>, ISourceableQuery<TModel>
        where TModel : class, IModel, new()
    {
    }
}