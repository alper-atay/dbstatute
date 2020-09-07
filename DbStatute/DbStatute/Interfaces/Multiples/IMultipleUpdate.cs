using DbStatute.Interfaces.Fundamentals.Multiples;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleUpdate : IMultipleUpdateBase, ISourceModels
    {
    }

    public interface IMultipleUpdate<TModel> : IMultipleUpdateBase<TModel>, ISourceModels<TModel>, IMultipleUpdate
        where TModel : class, IModel, new()
    {
    }
}