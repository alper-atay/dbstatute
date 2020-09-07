using DbStatute.Interfaces.Fundamentals.Multiples;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleUpdateBySourceModels : IMultipleUpdateBase, ISourceModels
    {
    }

    public interface IMultipleUpdateBySourceModels<TModel> : IMultipleUpdateBase<TModel>, ISourceModels<TModel>, IMultipleUpdateBySourceModels
        where TModel : class, IModel, new()
    {
    }
}