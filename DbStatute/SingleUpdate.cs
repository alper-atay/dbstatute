using DbStatute.Interfaces;
using DbStatute.Querying;
using System;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute
{
    public abstract class SingleUpdate<TId, TModel, TUpdateQuery> : Update
        where TId : struct, IConvertible
        where TModel : class, IModel<TId>, new()
        where TUpdateQuery : UpdateQuery<TId, TModel>
    {
        protected SingleUpdate(TUpdateQuery updateQuery)
        {
            UpdateQuery = updateQuery ?? throw new ArgumentNullException(nameof(updateQuery));
        }

        public override int UpdatedCount => UpdatedModel is null ? 0 : 1;
        public TModel UpdatedModel { get; private set; }
        public TUpdateQuery UpdateQuery { get; }

        public async Task<TModel> UpdateAsync(IDbConnection dbConnection)
        {
            Logs.AddRange(UpdateQuery.Test());

            if(ReadOnlyLogs.Safely)
            {
                // We have will update fields
                var updateFields = UpdateQuery.UpdateFields;

                // We have update model
                // Now we dont have Id :/
                var updateModel = UpdateQuery.Model;
            }


            return null;
        }
    }
}