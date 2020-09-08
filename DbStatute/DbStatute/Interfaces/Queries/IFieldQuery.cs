using DbStatute.Interfaces.Qualifiers;

namespace DbStatute.Interfaces.Queries
{
    public interface IFieldQuery
    {
        IFieldQualifier Fields { get; }
    }

    public interface IFieldQuery<TModel> : IFieldQuery
        where TModel : class, IModel, new()
    {
        new IFieldQualifier<TModel> Fields { get; }
    }
}