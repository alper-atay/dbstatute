using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying;
using RepoDb;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DbStatute
{
    public abstract class SingleSelectByQuery<TId, TModel, TSelectQuery> : SingleSelect<TId, TModel>, ISingleSelectByQuery<TId, TModel, TSelectQuery>
        where TId : notnull, IConvertible
        where TModel : class, IModel<TId>, new()
        where TSelectQuery : ISelectQuery<TId, TModel>
    {
        protected SingleSelectByQuery(TSelectQuery selectQuery)
        {
            SelectQuery = selectQuery;
        }

        public TSelectQuery SelectQuery { get; }

        protected override Task<TModel> SelectOperationAsync(IDbConnection dbConnection)
        {
            Logs.AddRange(SelectQuery.Test());
            if (ReadOnlyLogs.Safely)
            {
                QueryGroup queryGroup = SelectQuery.QueryGroup;
                int queryFieldCount = queryGroup.GetFields(true).Count();

                if (queryFieldCount > 0)
                {
                    return dbConnection.QueryAsync<TModel>(SelectQuery.QueryGroup, top: 1)
                        .ContinueWith(x => x.Result.FirstOrDefault());
                }
                else
                {
                    Logs.Warning($"{GetType().FullName} class, query group does not contain query fields!");
                }

                return null;
            }

            return null;
        }
    }
}