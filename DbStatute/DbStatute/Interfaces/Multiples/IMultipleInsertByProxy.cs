using DbStatute.Interfaces.Fundamentals.Multiples;
using DbStatute.Interfaces.Proxies;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleInsertByProxy<TInsertProxy> : IMultipleInsertBase
        where TInsertProxy : IInsertProxy
    {
        TInsertProxy InsertProxy { get; }
    }

    public interface IMultipleInsertByProxy<TModel, TInsertProxy> : IMultipleInsertBase<TModel>, IMultipleInsertByProxy<TInsertProxy>
        where TModel : class, IModel, new()
        where TInsertProxy : IInsertProxy<TModel>
    {
        new TInsertProxy InsertProxy { get; }
    }
}