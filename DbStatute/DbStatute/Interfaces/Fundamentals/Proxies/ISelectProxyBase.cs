namespace DbStatute.Interfaces.Fundamentals.Proxies
{
    public interface ISelectProxyBase : IProxyBase
    {
    }

    public interface ISelectProxyBase<TModel> : IProxyBase<TModel>, ISelectProxyBase
        where TModel : class, IModel, new()
    {
    }
}