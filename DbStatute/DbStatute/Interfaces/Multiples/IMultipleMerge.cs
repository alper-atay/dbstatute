using DbStatute.Interfaces.Fundamentals.Multiples;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleMerge : IMultipleMergeBase, ISourceModels
    {
    }

    public interface IMultipleMerge<TModel> : IMultipleMergeBase<TModel>, ISourceModels<TModel>, IMultipleMerge
        where TModel : class, IModel, new()
    {
    }
}