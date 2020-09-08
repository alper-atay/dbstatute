using DbStatute.Interfaces.Fundamentals.Queries;
using DbStatute.Interfaces.Fundamentals.Singles;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleDelete : ISingleDeleteBase, ISourceableModelQuery
    {
    }

    public interface ISingleDelete<TModel> : ISingleDeleteBase<TModel>, ISourceableModelQuery<TModel>
        where TModel : class, IModel, new()
    {
    }
}