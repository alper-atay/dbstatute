using DbStatute.Interfaces.Fundamentals.Multiples;
using DbStatute.Interfaces.Fundamentals.Queries;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleInsert : IMultipleInsertBase, ISourceableModelsQuery
    {
    }

    public interface IMultipleInsert<TModel> : IMultipleInsertBase<TModel>, ISourceableModelsQuery<TModel>, IMultipleInsert
        where TModel : class, IModel, new()
    {
    }
}