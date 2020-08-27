using DbStatute.Interfaces.Fundamentals.Multiples;
using DbStatute.Interfaces.Querying;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleInsertByQuery<TInsertQuery> : IMultipleInsertBase
        where TInsertQuery : IInsertQuery
    {
        TInsertQuery InsertQuery { get; }
    }

    public interface IMultipleInsertByQuery<TModel, TInsertQuery> : IMultipleInsertBase<TModel>, IMultipleInsertByQuery<TInsertQuery>
        where TModel : class, IModel, new()
        where TInsertQuery : IInsertQuery<TModel>
    {
        new TInsertQuery InsertQuery { get; }
    }
}