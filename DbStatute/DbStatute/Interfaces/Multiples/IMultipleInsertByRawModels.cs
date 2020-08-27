using DbStatute.Interfaces.Fundamentals.Multiples;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleInsertByRawModels : IMultipleInsertBase, IRawModels
    {
    }

    public interface IMultipleInsertByRawModels<TModel> : IMultipleInsertBase<TModel>, IRawModels<TModel>, IMultipleInsertByRawModels
        where TModel : class, IModel, new()
    {
    }
}