using DbStatute.Interfaces.Fundamentals.Proxies;
using DbStatute.Interfaces.Querying.Builders;

namespace DbStatute.Interfaces.Proxies
{
    public interface IMergeProxy : IStatuteProxyBase
    {
        IModelBuilder ModelBuilder { get; }
    }

    public interface IMergeProxy<TModel> : IStatuteProxyBase<TModel>, IMergeProxy
        where TModel : class, IModel, new()
    {
        new IModelBuilder<TModel> ModelBuilder { get; }
    }
}