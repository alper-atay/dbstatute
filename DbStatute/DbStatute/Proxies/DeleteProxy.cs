using DbStatute.Fundamentals.Proxies;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies;

namespace DbStatute.Proxies
{
    public class DeleteProxy : DeleteProxyBase, IDeleteProxy
    {

    }

    public class DeleteProxy<TModel> : DeleteProxyBase<TModel>, IDeleteProxy<TModel>
        where TModel : class, IModel, new()
    {

    }
}