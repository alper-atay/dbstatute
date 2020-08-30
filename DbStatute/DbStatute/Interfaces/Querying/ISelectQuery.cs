using DbStatute.Interfaces.Querying.Builders;
using DbStatute.Interfaces.Querying.Fundamentals;
using DbStatute.Interfaces.Querying.Qualifiers.Fields;

namespace DbStatute.Interfaces.Querying
{
    public interface ISelectQuery : IStatuteQueryBase
    {
        public ISelectQueryGroupBuilder SelectQueryGroupBuilder { get; }
        public IOrderFieldQualifier OrderFieldQualifier { get; }
    }

    public interface ISelectQuery<TModel> : IStatuteQueryBase<TModel>, ISelectQuery
        where TModel : class, IModel, new()
    {
        new ISelectQueryGroupBuilder<TModel> SelectQueryGroupBuilder { get; }
        new IOrderFieldQualifier<TModel> OrderFieldQualifier { get; }
    }
}