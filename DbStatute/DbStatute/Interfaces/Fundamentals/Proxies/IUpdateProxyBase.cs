namespace DbStatute.Interfaces.Fundamentals.Proxies
{
    public interface IUpdateProxyBase : IProxyBase
    {
    }

    public interface IUpdateProxyBase<TModel> : IProxyBase<TModel>
    where TModel : class, IModel, new()
    {
    }
}