using DbStatute.Interfaces.Fundamentals.Singles;
using DbStatute.Interfaces.Proxies;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleInsertByQuery : ISingleInsertBase
    {
        IInsertProxy InsertProxy { get; }
    }

    public interface ISingleInsertByProxy<TModel> : ISingleInsertBase<TModel>, ISingleInsertByQuery
        where TModel : class, IModel, new()
    {
        new IInsertProxy<TModel> InsertProxy { get; }
    }


}