namespace DbStatute.Interfaces
{
    public interface IDelete : IStatute
    {
        int DeletedCount { get; }
    }
}