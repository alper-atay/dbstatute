using DbStatute.Interfaces.Fundamentals.Singles;
using DbStatute.Interfaces.Proxies;

namespace DbStatute.Interfaces.Singles
{
    public interface ISingleUpdateByProxyWithSelect : ISingleUpdateBase, ISourceModel
    {
        IUpdateProxyWithSelect UpdateProxyWithSelect { get; }
    }

    public interface ISingleUpdateByProxyWithSelect<TModel> : ISingleUpdateBase<TModel>, ISingleUpdateByProxyWithSelect
        where TModel : class, IModel, new()
    {
        new IUpdateProxyWithSelect<TModel> UpdateProxyWithSelect { get; }
    }
}