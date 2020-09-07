namespace DbStatute.Interfaces.Fundamentals.Proxies
{
    public interface IInsertProxyBase : IProxyBase
    {
    }

    public interface IInsertProxyBase<TModel> : IProxyBase<TModel>, IInsertProxyBase
        where TModel : class, IModel, new()
    {
    }
}