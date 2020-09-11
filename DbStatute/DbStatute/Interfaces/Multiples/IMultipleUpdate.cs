using DbStatute.Interfaces.Fundamentals.Multiples;
using DbStatute.Interfaces.Queries;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleUpdate : IMultipleUpdateBase, ISourceableModelsQuery
    {
    }

    public interface IMultipleUpdate<TModel> : IMultipleUpdateBase<TModel>, ISourceableModelsQuery<TModel>, IMultipleUpdate
        where TModel : class, IModel, new()
    {
    }
}