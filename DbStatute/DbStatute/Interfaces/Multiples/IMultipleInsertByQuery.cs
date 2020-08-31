using DbStatute.Interfaces.Fundamentals.Multiples;
using DbStatute.Interfaces.Proxies;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleInsertByQuery<TInsertQuery> : IMultipleInsertBase
        where TInsertQuery : IInsertProxy
    {
        TInsertQuery InsertQuery { get; }
    }

    public interface IMultipleInsertByQuery<TModel, TInsertQuery> : IMultipleInsertBase<TModel>, IMultipleInsertByQuery<TInsertQuery>
        where TModel : class, IModel, new()
        where TInsertQuery : IInsertProxy<TModel>
    {
        new TInsertQuery InsertQuery { get; }
    }
}