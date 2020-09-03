using DbStatute.Fundamentals.Multiples;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Multiples;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Multiples
{
    public class MultipleUpdateByRawModels<TModel> : MultipleUpdateBase<TModel>, IMultipleUpdateByRawModels<TModel>
        where TModel : class, IModel, new()
    {
        public IEnumerable<TModel> RawModels { get; }

        public MultipleUpdateByRawModels(IEnumerable<TModel> rawModels)
        {
            RawModels = rawModels ?? throw new ArgumentNullException(nameof(rawModels));
        }

        IEnumerable<object> IRawModels.RawModels => RawModels;

        protected override IAsyncEnumerable<TModel> UpdateAsSinglyOperationAsync(IDbConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        protected override Task<IEnumerable<TModel>> UpdateOperationAsync(IDbConnection dbConnection)
        {
            throw new NotImplementedException();
        }
    }
}