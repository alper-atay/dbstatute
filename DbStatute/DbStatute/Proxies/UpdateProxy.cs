using DbStatute.Fundamentals.Proxies;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Proxies;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Qualifiers;
using System;

namespace DbStatute.Proxies
{
    public class UpdateProxy : UpdateProxyBase, IUpdateProxy
    {
    }

    public class UpdateProxy<TModel> : UpdateProxyBase<TModel>, IUpdateProxy<TModel>
        where TModel : class, IModel, new()
    {

    }
}