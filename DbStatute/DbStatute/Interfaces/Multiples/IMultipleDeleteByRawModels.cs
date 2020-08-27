using DbStatute.Interfaces.Fundamentals;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleDeleteByRawModel : IMultipleDeleteBase, IRawModels
    {
    }

    public interface IMultipleDeleteByRawModels<TModel> : IMultipleDeleteBase<TModel>, IRawModels<TModel>, IMultipleDeleteByRawModel
        where TModel : class, IModel, new()
    {
    }
}