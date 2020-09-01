using DbStatute.Interfaces.Fundamentals.Multiples;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleSelectByIds : IMultipleSelectBase, IIds
    {
    }

    public interface IMultipleSelectByIds<TModel> : IMultipleSelectBase<TModel>, IMultipleSelectByIds
        where TModel : class, IModel, new()
    {
    }
}