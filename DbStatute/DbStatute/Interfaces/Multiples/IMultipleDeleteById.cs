using DbStatute.Interfaces.Fundamentals;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleDeleteById : IMultipleDeleteBase, IIds
    {
    }

    public interface IMultipleDeleteById<TModel> : IMultipleDeleteBase<TModel>, IMultipleDeleteById
        where TModel : class, IModel, new()
    {
    }
}