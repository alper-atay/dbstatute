using DbStatute.Interfaces.Querying.Qualifiers.Fields;

namespace DbStatute.Interfaces.Querying.Builders
{
    public interface IOrderFieldBuilder
    {
        IOrderFieldQualifier OrderFieldQualifier { get; }
    }

    public interface IOrderFieldBuilder<TModel> : IOrderFieldBuilder
        where TModel : class, IModel, new()
    {
        new IOrderFieldQualifier<TModel> OrderFieldQualifier { get; }
    }
}
