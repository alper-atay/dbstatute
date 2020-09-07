using DbStatute.Interfaces.Fundamentals.Multiples;
using DbStatute.Interfaces.Proxies;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleUpdateByProxy : IMultipleUpdateBase, ISourceModels
    {
        IUpdateProxy UpdateProxy { get; }
    }

    public interface IMultipleUpdateByProxy<TModel> : IMultipleUpdateBase<TModel>, ISourceModels<TModel>, IMultipleUpdateByProxy
        where TModel : class, IModel, new()
    {
        new IUpdateProxy<TModel> UpdateProxy { get; }
    }
}