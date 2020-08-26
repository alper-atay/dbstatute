namespace DbStatute.Interfaces
{
    public interface ISelect : IStatute
    {
        public ICacheable Cacheable { get; set; }
        int SelectedCount { get; }
    }

    public interface ISelect<TModel> : IStatute<TModel>, ISelect
        where TModel : class, IModel, new()
    {
    }
}