using DbStatute.Interfaces.Fundamentals.Proxies;
using DbStatute.Interfaces.Qualifiers;

namespace DbStatute.Interfaces.Proxies
{
    public interface IInsertProxy : IInsertProxyBase
    {
    }

    public interface IInsertProxy<TModel> : IInsertProxyBase<TModel>, IInsertProxy
        where TModel : class, IModel, new()
    {
    }
}