using DbStatute.Fundamentals.Proxies;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies;

namespace DbStatute.Proxies
{
    public class InsertProxy : InsertProxyBase, IInsertProxy
    {
    }

    public class InsertProxy<TModel> : InsertProxyBase<TModel>, IInsertProxy<TModel>
        where TModel : class, IModel, new()
    {
    }
}