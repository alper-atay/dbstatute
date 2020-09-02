using DbStatute.Interfaces.Fundamentals.Singles;
using DbStatute.Interfaces.Proxies;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleInsertByProxy : ISingleInsertBase
    {
        IInsertProxy InsertProxy { get; }
    }

    public interface ISingleInsertByProxy<TModel> : ISingleInsertBase<TModel>, ISingleInsertByProxy
        where TModel : class, IModel, new()
    {
        new IInsertProxy<TModel> InsertProxy { get; }
    }
}