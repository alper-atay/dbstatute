using DbStatute.Fundamentals.Proxies;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies;

namespace DbStatute.Querying
{
    public class InsertProxy : StatuteProxyBase, IInsertProxy
    {

    }

    public class InsertProxy<TModel> : StatuteProxyBase<TModel>, IInsertProxy<TModel>
        where TModel : class, IModel, new()
    {

    }
}