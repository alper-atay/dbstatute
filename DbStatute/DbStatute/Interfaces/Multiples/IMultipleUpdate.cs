using DbStatute.Interfaces.Fundamentals.Multiples;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleUpdate : IMultipleUpdateBase, IReadyModels
    {
    }

    public interface IMultipleUpdate<TModel> : IMultipleUpdateBase<TModel>, IReadyModels<TModel>, IMultipleUpdate
        where TModel : class, IModel, new()
    {
    }
}