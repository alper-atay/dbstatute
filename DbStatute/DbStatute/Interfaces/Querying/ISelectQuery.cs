using DbStatute.Interfaces.Querying.Fundamentals;
using DbStatute.Interfaces.Querying.Qualifiers.Fields;

namespace DbStatute.Interfaces.Querying
{
    public interface ISelectQuery : IStatuteQueryBase
    {
        public IOperationalQueryQualifier OperationalQueryQualifier { get; }
        public IOrderFieldQualifier OrderFieldQualifier { get; }
    }

    public interface ISelectQuery<TModel> : IStatuteQueryBase<TModel>, ISelectQuery
        where TModel : class, IModel, new()
    {
        new IOperationalQueryQualifier<TModel> OperationalQueryQualifier { get; }
        new IOrderFieldQualifier<TModel> OrderFieldQualifier { get; }
    }
}