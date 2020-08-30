namespace DbStatute.Interfaces.Querying.Builders
{
    public interface IMergeQueryGroupBuilder : IQueryGroupBuilder
    {
    }

    public interface IMergeQueryGroupBuilder<TModel> : IQueryGroupBuilder<TModel>, IMergeQueryGroupBuilder
        where TModel : class, IModel, new()
    {
        
    }
}