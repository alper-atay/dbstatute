using DbStatute.Interfaces.Fundamentals.Multiples;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleUpdateByRawModels : IMultipleUpdateBase, IRawModels
    {
    }

    public interface IMultipleUpdateByRawModels<TModel> : IMultipleUpdateBase<TModel>, IRawModels<TModel>, IMultipleUpdateByRawModels
        where TModel : class, IModel, new()
    {
    }
}