using DbStatute.Interfaces.Fundamentals.Proxies;
using DbStatute.Interfaces.Querying.Builders;

namespace DbStatute.Interfaces.Proxies
{
    public interface IInsertProxy : IStatuteProxyBase
    {
        IModelBuilder ModelBuilder { get; }
    }

    public interface IInsertProxy<TModel> : IStatuteProxyBase<TModel>, IInsertProxy
        where TModel : class, IModel, new()
    {
        new IModelBuilder<TModel> ModelBuilder { get; }
    }
}