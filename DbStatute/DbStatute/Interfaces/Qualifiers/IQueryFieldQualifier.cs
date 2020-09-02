using DbStatute.Interfaces.Fundamentals.Enumerables;
using DbStatute.Interfaces.Utilities;

namespace DbStatute.Interfaces.Qualifiers
{
    public interface IQueryFieldQualifier : ISettableQueryField, IQueryFieldCollection
    {
    }

    public interface IQueryFieldQualifier<TModel> : ISettableQueryField<TModel>
        where TModel : class, IModel, new()
    {
    }
}