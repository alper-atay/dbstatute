using DbStatute.Interfaces.Querying.Fundamentals.Qualifiers;

namespace DbStatute.Interfaces.Querying.Qualifiers
{
    public interface IValueFieldQualifier : ISettableSpecializedField<object>, IFieldValueMap
    {
        IFieldQualifier FieldQualifier { get; }
    }

    public interface IValueFieldQualifier<TModel> : ISettableSpecializedField<TModel, object>, IValueFieldQualifier
        where TModel : class, IModel, new()
    {
        new IFieldQualifier<TModel> FieldQualifier { get; }
    }
}