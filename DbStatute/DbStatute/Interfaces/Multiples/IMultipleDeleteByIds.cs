using DbStatute.Interfaces.Fundamentals;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleDeleteByIds : IMultipleDeleteBase, IIds
    {
    }

    public interface IMultipleDeleteByIds<TModel> : IMultipleDeleteBase<TModel>, IMultipleDeleteByIds
        where TModel : class, IModel, new()
    {
    }
}