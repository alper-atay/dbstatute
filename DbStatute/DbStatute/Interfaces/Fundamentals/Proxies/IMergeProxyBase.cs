namespace DbStatute.Interfaces.Fundamentals.Proxies
{
    public interface IMergeProxyBase : IProxyBase
    {
    }

    public interface IMergeProxyBase<TModel> : IProxyBase<TModel>, IMergeProxyBase
        where TModel : class, IModel, new()
    {
    }
}