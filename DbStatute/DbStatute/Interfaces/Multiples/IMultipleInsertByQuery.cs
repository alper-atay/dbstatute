using DbStatute.Interfaces.Fundamentals.Multiples;
using DbStatute.Interfaces.Querying;

namespace DbStatute.Interfaces.Multiples
{
    public interface IMultipleInsertByQuery<TModel, TInsertQoery> : IMultipleInsertBase<TModel>
        where TModel : class, IModel, new()
    {
        IInsertQuery<TModel> InsertQuery { get; }
    }
}