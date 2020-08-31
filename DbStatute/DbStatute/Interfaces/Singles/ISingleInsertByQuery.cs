using DbStatute.Interfaces.Fundamentals.Singles;
using DbStatute.Interfaces.Proxies;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleInsertByQuery<TInsertQuery> : ISingleInsertBase
        where TInsertQuery : IInsertProxy
    {
        TInsertQuery InsertQuery { get; }
    }

    public interface ISingleInsertByQuery<TModel, TInsertQuery> : ISingleInsertBase<TModel>, ISingleInsertByQuery<TInsertQuery>
        where TModel : class, IModel, new()
        where TInsertQuery : IInsertProxy<TModel>
    {
        new TInsertQuery InsertQuery { get; }
    }
}