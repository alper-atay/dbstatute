using DbStatute.Interfaces.Fundamentals;
using DbStatute.Interfaces.Querying;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleMergeByQuery<TModel, TMergeQuery> : IMergeBase
        where TModel : class, IModel, new()
        where TMergeQuery : IMergeQuery<TModel>
    {
    }
}