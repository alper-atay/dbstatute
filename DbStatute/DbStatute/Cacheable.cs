using DbStatute.Interfaces;
using RepoDb;
using RepoDb.Interfaces;

namespace DbStatute
{
    public sealed class Cacheable : ICacheable
    {
        public ICache Cache { get; set; } = null;
        public int? ItemExpiration { get; set; } = null;
        public string Key { get; set; } = null;

    }
}