using DbStatute.Interfaces.Fundamentals.Multiples;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleInsert : IMultipleInsertBase, ISourceModels
    {
    }

    public interface IMultipleInsert<TModel> : IMultipleInsertBase<TModel>, ISourceModels<TModel>, IMultipleInsert
        where TModel : class, IModel, new()
    {
    }
}