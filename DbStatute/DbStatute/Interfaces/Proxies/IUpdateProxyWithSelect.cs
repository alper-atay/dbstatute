using System;
using System.Collections.Generic;
using System.Text;

namespace DbStatute.Interfaces.Proxies
{
    public interface IUpdateProxyWithSelect : IUpdateProxy
    {
        ISelectProxy SelectProxy { get; }
    }

    public interface IUpdateProxyWithSelect<TModel> : IUpdateProxy<TModel>, IUpdateProxyWithSelect
        where TModel : class, IModel, new()
    {
        new ISelectProxy<TModel> SelectProxy { get; }
    }
}
