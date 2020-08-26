namespace DbStatute.Interfaces
{
    public interface ISelect : IStatute
    {
        public ICacheable Cacheable { get; set; }
        int SelectedCount { get; }
    }
}