using DbStatute.Interfaces.Fundamentals.Singles;
using DbStatute.Interfaces.Proxies;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleSelectByProxy : ISingleSelectBase
    {
        ISelectProxy SelectProxy { get; }
    }

    public interface ISingleSelectByProxy<TModel> : ISingleSelectBase<TModel>, ISingleSelectByProxy
        where TModel : class, IModel, new()
    {
        new ISelectProxy<TModel> SelectProxy { get; }
    }
}