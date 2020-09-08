using DbStatute.Fundamentals.Proxies;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies;

namespace DbStatute.Proxies
{
    public class SelectProxy : SelectProxyBase, ISelectProxy
    {
    }

    public class SelectProxy<TModel> : SelectProxyBase<TModel>, ISelectProxy<TModel>
        where TModel : class, IModel, new()
    {
    }
}