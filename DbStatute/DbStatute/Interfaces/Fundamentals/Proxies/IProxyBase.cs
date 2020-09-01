namespace DbStatute.Interfaces.Fundamentals.Proxies
{
    public interface IProxyBase
    {
    }

    public interface IProxyBase<TModel> : IProxyBase
        where TModel : class, IModel, new()
    {
    }
}