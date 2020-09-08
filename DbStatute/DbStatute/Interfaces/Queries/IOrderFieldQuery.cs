using DbStatute.Interfaces.Qualifiers;

namespace DbStatute.Interfaces.Queries
{
    public interface IOrderFieldQuery
    {
        IOrderFieldQualifier OrderFields { get; }
    }

    public interface IOrderFieldQuery<TModel> : IOrderFieldQuery
        where TModel : class, IModel, new()
    {
        new IOrderFieldQualifier<TModel> OrderFields { get; }
    }
}