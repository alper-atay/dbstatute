using DbStatute.Interfaces.Fundamentals.Proxies;

namespace DbStatute.Interfaces.Proxies
{
    public interface IDeleteProxy : IProxyBase
    {
        ISelectProxy SelectProxy { get; }
    }

    public interface IDeleteProxy<TModel> : IProxyBase<TModel>, IDeleteProxy
        where TModel : class, IModel, new()
    {
        new ISelectProxy<TModel> SelectProxy { get; }
    }
}