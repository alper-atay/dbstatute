namespace DbStatute.Interfaces.Querying.Statutes
{
    public interface ISelectQuery : IStatuteQuery
    {
        public IOperationalQueryQualifier OperationalQueryQualifier { get; }
        public IOrderFieldQualifier OrderFieldQualifier { get; }
    }

    public interface ISelectQuery<TModel> : IStatuteQuery<TModel>, ISelectQuery
        where TModel : class, IModel, new()
    {
        new IOperationalQueryQualifier<TModel> OperationalQueryQualifier { get; }
        new IOrderFieldQualifier<TModel> OrderFieldQualifier { get; }
    }
}