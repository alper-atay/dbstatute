using DbStatute.Interfaces.Fundamentals.Singles;
using DbStatute.Interfaces.Proxies;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleInsertByQuery<TInsertQuery> : ISingleInsertBase
        where TInsertQuery : IInsertProxy
    {
        TInsertQuery InsertProxy { get; }
    }

    public interface ISingleInsertByProxy<TModel, TInsertQuery> : ISingleInsertBase<TModel>, ISingleInsertByQuery<TInsertQuery>
        where TModel : class, IModel, new()
        where TInsertQuery : IInsertProxy<TModel>
    {
        new TInsertQuery InsertProxy { get; }
    }
}