using DbStatute.Interfaces.Fundamentals.Multiples;
using DbStatute.Interfaces.Proxies;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleInsertByProxyOnReadyModels : IMultipleInsertBase, IReadyModels
    {
        IInsertProxy InsertProxy { get; }
    }

    public interface IMultipleInsertByProxyOnReadyModels<TModel> : IMultipleInsertBase<TModel>, IReadyModels<TModel>, IMultipleInsertByProxyOnReadyModels
        where TModel : class, IModel, new()
    {
        new IInsertProxy<TModel> InsertProxy { get; }
    }
}