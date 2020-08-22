using DbStatute.Interfaces;
using RepoDb;
using RepoDb.Enumerations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute
{
    public abstract class MultipleSelectById<TId, TModel> : MultipleSelect<TId, TModel>
        where TId : struct, IConvertible
        where TModel : class, IModel<TId>
    {
        protected MultipleSelectById(IEnumerable<TId> ids)
        {
            Ids = ids ?? throw new ArgumentNullException(nameof(ids));
        }

        public IEnumerable<TId> Ids { get; }

        protected override async Task<IEnumerable<TModel>> SelectOperationAsync(IDbConnection dbConnection)
        {
            if (ReadOnlyLogs.Safely)
            {
                QueryField queryFieldById = new QueryField(new Field(nameof(IModel<TId>.Id)), Operation.In, Ids);

                return await dbConnection.QueryAsync<TModel>(queryFieldById);
            }

            return null;
        }
    }
}