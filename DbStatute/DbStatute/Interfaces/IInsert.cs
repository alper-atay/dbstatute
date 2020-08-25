namespace DbStatute.Interfaces
{
    public interface IInsert : IStatute
    {
        int InsertedCount { get; }
    }
}