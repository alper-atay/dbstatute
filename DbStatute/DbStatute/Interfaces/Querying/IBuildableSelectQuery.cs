using RepoDb;

namespace DbStatute.Interfaces.Querying
{
    public interface IBuildableSelectQuery
    {
        QueryGroup Build();
    }
}