using DbStatute.Interfaces.Fundamentals.Proxies;
using DbStatute.Interfaces.Querying.Builders;

namespace DbStatute.Interfaces.Proxies
{
    public interface IInsertProxy : IStatuteProxyBase
    {
    }

    public interface IInsertProxy<TModel> : IStatuteProxyBase<TModel>, IInsertProxy
        where TModel : class, IModel, new()
    {
    }
}