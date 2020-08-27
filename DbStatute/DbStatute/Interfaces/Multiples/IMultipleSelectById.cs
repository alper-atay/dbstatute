using DbStatute.Interfaces.Fundamentals;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleSelectById : IMultipleSelectBase, IIds
    {
    }

    public interface IMultipleSelectById<TModel> : IMultipleSelectBase<TModel>, IMultipleSelectById
        where TModel : class, IModel, new()
    {
    }
}