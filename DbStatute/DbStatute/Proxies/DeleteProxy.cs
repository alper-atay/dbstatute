using DbStatute.Fundamentals.Proxies;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies;

namespace DbStatute.Proxies
{
    public class DeleteProxy : StatuteProxyBase, IDeleteProxy
    {
        public DeleteProxy()
        {
        }
    }

    public class DeleteProxy<TModel> : StatuteProxyBase<TModel>, IDeleteProxy<TModel>
        where TModel : class, IModel, new()
    {
        public DeleteProxy()
        {
        }

    }
}