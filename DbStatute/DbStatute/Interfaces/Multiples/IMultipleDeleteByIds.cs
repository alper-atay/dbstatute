using DbStatute.Interfaces.Fundamentals.Multiples;
using DbStatute.Interfaces.Fundamentals.Queries;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleDeleteByIds : IMultipleDeleteBase, IIdentifiablesQuery
    {
    }

    public interface IMultipleDeleteByIds<TModel> : IMultipleDeleteBase<TModel>, IMultipleDeleteByIds
        where TModel : class, IModel, new()
    {
    }
}