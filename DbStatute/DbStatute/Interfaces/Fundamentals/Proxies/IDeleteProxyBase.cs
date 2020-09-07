namespace DbStatute.Interfaces.Fundamentals.Proxies
{
    public interface IDeleteProxyBase : IProxyBase
    {
    }

    public interface IDeleteProxyBase<TModel> : IProxyBase<TModel>, IDeleteProxyBase
        where TModel : class, IModel, new()
    {
    }
}