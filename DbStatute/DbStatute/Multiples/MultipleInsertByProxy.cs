using DbStatute.Fundamentals.Multiples;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Multiples;
using DbStatute.Interfaces.Proxies;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Multiples
{
    public class MultipleInsertByProxy<TModel, TInsertProxy> : MultipleInsertBase<TModel>, IMultipleInsertByProxy<TModel, TInsertProxy>
        where TModel : class, IModel, new()
        where TInsertProxy : IInsertProxy<TModel>
    {
        public TInsertProxy InsertProxy { get; }

        public MultipleInsertByProxy(TInsertProxy insertProxy)
        {
            InsertProxy = insertProxy ?? throw new ArgumentNullException(nameof(insertProxy));
        }

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