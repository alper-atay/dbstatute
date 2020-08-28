namespace DbStatute.Interfaces.Querying.Builders
{
    public interface IMergeQueryGroupBuilder : IQueryGroupBuilder
    {
    }

    public interface IMergeQueryGroupBuilder<TModel> : IQueryGroupBuilder
        where TModel : class, IModel, new()
    {
    }
}