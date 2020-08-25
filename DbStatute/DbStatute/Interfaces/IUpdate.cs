namespace DbStatute.Interfaces
{
    public interface IUpdate : IStatute
    {
        int UpdatedCount { get; }
    }
}