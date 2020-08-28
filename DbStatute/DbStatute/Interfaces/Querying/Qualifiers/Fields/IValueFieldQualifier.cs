using DbStatute.Interfaces.Querying.Fundamentals.Maps;
using DbStatute.Interfaces.Querying.Fundamentals.Qualifiers;

namespace DbStatute.Interfaces.Querying.Qualifiers.Fields
{
    public interface IValueFieldQualifier : ISettableSpecializedField<object>, IFieldValueMap
    {
    }

    public interface IValueFieldQualifier<TModel> : ISettableSpecializedField<TModel, object>, IValueFieldQualifier
        where TModel : class, IModel, new()
    {
    }
}