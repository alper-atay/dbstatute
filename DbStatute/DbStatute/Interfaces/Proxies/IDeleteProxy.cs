using DbStatute.Interfaces.Fundamentals.Proxies;

namespace DbStatute.Interfaces.Proxies
{
    public interface IDeleteProxy : IStatuteProxyBase
    {
        ISelectProxy SelectProxy { get; }
    }

    public interface IDeleteProxy<TModel> : IStatuteProxyBase<TModel>, IDeleteProxy
        where TModel : class, IModel, new()
    {
        new ISelectProxy<TModel> SelectProxy { get; }
    }
}