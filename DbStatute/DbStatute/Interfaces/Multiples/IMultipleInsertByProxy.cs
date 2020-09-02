using DbStatute.Interfaces.Fundamentals.Multiples;
using DbStatute.Interfaces.Proxies;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleInsertByProxy : IMultipleInsertBase
    {
        IInsertProxy InsertProxy { get; }
    }

    public interface IMultipleInsertByProxy<TModel> : IMultipleInsertBase<TModel>, IMultipleInsertByProxy
        where TModel : class, IModel, new()
    {
        new IInsertProxy<TModel> InsertProxy { get; }
    }
}