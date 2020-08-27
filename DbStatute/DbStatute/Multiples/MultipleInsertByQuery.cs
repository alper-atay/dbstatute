using DbStatute.Fundamentals.Multiples;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Multiples;
using DbStatute.Interfaces.Querying;
using DbStatute.Querying;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute
{
    public class MultipleInsertByQuery<TModel, TInsertQuery> : MultipleInsertBase<TModel>, IMultipleInsertByQuery<TModel, TInsertQuery>
        where TModel : class, IModel, new()
        where TInsertQuery : ISelectQuery<TModel>
    {
        public MultipleInsertByQuery()
        {
            InsertQuery = new InsertQuery<TModel>();
        }

        public IInsertQuery<TModel> InsertQuery { get; }

        protected override IAsyncEnumerable<TModel> InsertAsSingleOperationAsync(IDbConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        protected override Task<IEnumerable<TModel>> InsertOperationAsync(IDbConnection dbConnection)
        {
            throw new NotImplementedException();
        }
    }
}