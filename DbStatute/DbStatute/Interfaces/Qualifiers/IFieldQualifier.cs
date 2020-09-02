using DbStatute.Interfaces.Fundamentals.Enumerables;
using DbStatute.Interfaces.Utilities;

namespace DbStatute.Interfaces.Qualifiers
{
    public interface IFieldQualifier : ISettableField, IFieldCollection
    {
    }

    public interface IFieldQualifier<TModel> : ISettableField<TModel>, IFieldQualifier
        where TModel : class, IModel, new()
    {
    }
}