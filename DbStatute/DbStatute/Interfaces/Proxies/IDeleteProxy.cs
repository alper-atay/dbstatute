using DbStatute.Interfaces.Fundamentals.Proxies;

namespace DbStatute.Interfaces.Proxies
{
    public interface IDeleteProxy : IStatuteProxyBase
    {

    }

    public interface IDeleteProxy<TModel> : IStatuteProxyBase<TModel>, IDeleteProxy
        where TModel : class, IModel, new()
    {

    }
}