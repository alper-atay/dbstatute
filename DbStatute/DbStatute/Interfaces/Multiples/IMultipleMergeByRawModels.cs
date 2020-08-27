using DbStatute.Interfaces.Fundamentals.Multiples;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleMergeByRawModels : IMultipleMergeBase, IRawModels
    {
    }

    public interface IMultipleMergeByMergeModels<TModel> : IMultipleMergeBase<TModel>, IRawModels<TModel>, IMultipleMergeByRawModels
        where TModel : class, IModel, new()
    {
    }
}