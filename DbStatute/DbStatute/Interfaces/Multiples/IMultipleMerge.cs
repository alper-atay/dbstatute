using DbStatute.Interfaces.Fundamentals.Multiples;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleMerge : IMultipleMergeBase, IReadyModels
    {
    }

    public interface IMultipleMerge<TModel> : IMultipleMergeBase<TModel>, IReadyModels<TModel>, IMultipleMerge
        where TModel : class, IModel, new()
    {
    }
}