using DbStatute.Interfaces.Fundamentals.Proxies;

namespace DbStatute.Interfaces.Proxies
{
    public interface IDeleteProxy : IDeleteProxyBase
    {
        ISelectProxy SelectProxy { get; }
    }

    public interface IDeleteProxy<TModel> : IDeleteProxyBase<TModel>, IDeleteProxy
        where TModel : class, IModel, new()
    {
        new ISelectProxy<TModel> SelectProxy { get; }
    }
}