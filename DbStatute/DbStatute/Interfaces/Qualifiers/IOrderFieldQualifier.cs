using DbStatute.Interfaces.Fundamentals.Enumerables;
using DbStatute.Interfaces.Utilities;

namespace DbStatute.Interfaces.Qualifiers
{
    public interface IOrderFieldQualifier : ISettableOrderField, IOrderFields
    {

    }

    public interface IOrderFieldQualifier<TModel> : ISettableOrderField<TModel>, IOrderFieldQualifier
        where TModel : class, IModel, new()
    {
    }
}