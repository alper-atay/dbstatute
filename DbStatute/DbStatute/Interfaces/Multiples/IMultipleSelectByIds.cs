using DbStatute.Interfaces.Fundamentals.Multiples;
using DbStatute.Interfaces.Queries;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleSelectByIds : IMultipleSelectBase, IIdentifiablesQuery
    {
    }

    public interface IMultipleSelectByIds<TModel> : IMultipleSelectBase<TModel>, IMultipleSelectByIds
        where TModel : class, IModel, new()
    {
    }
}