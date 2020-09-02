using DbStatute.Interfaces.Fundamentals.Singles;
using DbStatute.Interfaces.Proxies;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleDeleteByProxy : ISingleDeleteBase
    {
        IDeleteProxy DeleteProxy { get; }
    }

    public interface ISingleDeleteByProxy<TModel> : ISingleDeleteBase<TModel>, ISingleDeleteByProxy
        where TModel : class, IModel, new()
    {
        new IDeleteProxy<TModel> DeleteProxy { get; }
    }
}