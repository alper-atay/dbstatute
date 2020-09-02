using DbStatute.Interfaces.Fundamentals.Multiples;
using DbStatute.Interfaces.Proxies;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleUpdateByProxy : IMultipleUpdateBase
    {
        IUpdateProxy UpdateProxy { get; }
    }

    public interface IMultipleUpdateByQuery<TModel> : IMultipleUpdateBase<TModel>, IMultipleUpdateByProxy
        where TModel : class, IModel, new()
    {
        new IUpdateProxy<TModel> UpdateProxy { get; }
    }
}