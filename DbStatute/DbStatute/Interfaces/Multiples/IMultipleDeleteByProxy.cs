using DbStatute.Interfaces.Fundamentals.Multiples;
using DbStatute.Interfaces.Proxies;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleDeleteByProxy : IMultipleDeleteBase
    {
        IDeleteProxy DeleteProxy { get; }
    }

    public interface IMultipleDeleteByProxy<TModel> : IMultipleDeleteBase<TModel>, IMultipleDeleteByProxy
        where TModel : class, IModel, new()
    {
        new IDeleteProxy<TModel> DeleteProxy { get; }
    }
}