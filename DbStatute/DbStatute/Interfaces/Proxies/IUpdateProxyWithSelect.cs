using DbStatute.Interfaces.Fundamentals.Proxies;

namespace DbStatute.Interfaces.Proxies
{
    public interface IUpdateProxyWithSelect : IUpdateProxyBase
    {
        ISelectProxy SelectProxy { get; }
    }

    public interface IUpdateProxyWithSelect<TModel> : IUpdateProxyBase<TModel>, IUpdateProxyWithSelect
        where TModel : class, IModel, new()
    {
        new ISelectProxy<TModel> SelectProxy { get; }
    }
}
