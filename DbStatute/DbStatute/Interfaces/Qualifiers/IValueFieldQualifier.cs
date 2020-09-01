using DbStatute.Interfaces.Fundamentals.Maps;
using DbStatute.Interfaces.Querying.Fundamentals.Qualifiers;

namespace DbStatute.Interfaces.Qualifiers
{
    public interface IValueFieldQualifier : ISettableSpecializedField<object>, IReadOnlyFieldValueMap
    {
    }

    public interface IValueFieldQualifier<TModel> : ISettableSpecializedField<TModel, object>, IValueFieldQualifier
        where TModel : class, IModel, new()
    {
    }
}