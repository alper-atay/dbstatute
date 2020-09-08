using DbStatute.Interfaces.Fundamentals.Singles;
using DbStatute.Interfaces.Proxies;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleUpdateByProxy : ISingleUpdateBase
    {
        IUpdateProxy UpdateProxy { get; }
    }

    public interface ISingleUpdateByProxy<TModel> : ISingleUpdateBase<TModel>, ISingleUpdateByProxy
        where TModel : class, IModel, new()
    {
        new IUpdateProxy<TModel> UpdateProxy { get; }
    }
}