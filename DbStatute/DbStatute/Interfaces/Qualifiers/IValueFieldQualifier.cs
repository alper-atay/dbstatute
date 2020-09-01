using DbStatute.Interfaces.Fundamentals.Enumerables;
using DbStatute.Interfaces.Querying.Fundamentals.Qualifiers;

namespace DbStatute.Interfaces.Qualifiers
{
    public interface IValueFieldQualifier : ISettableSpecializedField<object>, IFieldValuePairs
    {
    }

    public interface IValueFieldQualifier<TModel> : ISettableSpecializedField<TModel, object>, IValueFieldQualifier
        where TModel : class, IModel, new()
    {
    }
}