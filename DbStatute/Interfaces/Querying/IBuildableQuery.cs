using RepoDb;

namespace DbStatute.Interfaces.Querying
{
    public interface IBuildableQuery
    {
        QueryGroup Build();
    }
}