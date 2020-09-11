using DbStatute.Interfaces.Fundamentals.Multiples;
using DbStatute.Interfaces.Queries;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleMerge : IMultipleMergeBase, ISourceableModelsQuery
    {
    }

    public interface IMultipleMerge<TModel> : IMultipleMergeBase<TModel>, ISourceableModelsQuery<TModel>, IMultipleMerge
        where TModel : class, IModel, new()
    {
    }
}