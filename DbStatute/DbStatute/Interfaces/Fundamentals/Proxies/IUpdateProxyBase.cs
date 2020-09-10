namespace DbStatute.Interfaces.Fundamentals.Proxies
{
    public interface IUpdateProxyBase : IProxyBase
    {
    }

    public interface IUpdateProxyBase<TModel> : IProxyBase<TModel>, IUpdateProxyBase
    where TModel : class, IModel, new()
    {

    }
}