using DbStatute.Interfaces.Fundamentals.Multiples;
using DbStatute.Interfaces.Queries;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleDelete : IMultipleDeleteBase, ISourceableModelsQuery
    {
    }

    public interface IMultipleDelete<TModel> : IMultipleDeleteBase<TModel>, ISourceableModelsQuery<TModel>, IMultipleDelete
        where TModel : class, IModel, new()
    {
    }
}