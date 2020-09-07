using DbStatute.Interfaces.Fundamentals.Multiples;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleDelete : IMultipleDeleteBase, ISourceModels
    {
    }

    public interface IMultipleDelete<TModel> : IMultipleDeleteBase<TModel>, ISourceModels<TModel>, IMultipleDelete
        where TModel : class, IModel, new()
    {
    }
}