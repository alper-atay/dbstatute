using RepoDb.Interfaces;

namespace DbStatute.Interfaces
{
    public interface ICacheable
    {
        ICache Cache { get; set; }
        int? ItemExpiration { get; set; }
        string Key { get; set; }
    }
}