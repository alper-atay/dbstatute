using DbStatute.Interfaces.Fundamentals.Multiples;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleInsert : IMultipleInsertBase, IReadyModels
    {
    }

    public interface IMultipleInsert<TModel> : IMultipleInsertBase<TModel>, IReadyModels<TModel>
        where TModel : class, IModel, new()
    {
    }
}