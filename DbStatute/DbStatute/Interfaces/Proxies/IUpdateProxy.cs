using DbStatute.Interfaces.Fundamentals.Proxies;
using DbStatute.Interfaces.Querying.Builders;

namespace DbStatute.Interfaces.Proxies
{
    public interface IUpdateProxy : IStatuteProxyBase
    {

    }

    public interface IUpdateProxy<TModel> : IStatuteProxyBase<TModel>, IUpdateProxy
        where TModel : class, IModel, new()
    {

    }
}