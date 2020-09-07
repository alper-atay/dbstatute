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
        public MultipleUpdateByRawModels(IEnumerable<TModel> rawModels)
        {
            RawModels = rawModels ?? throw new ArgumentNullException(nameof(rawModels));
        }


        public IEnumerable<TModel> RawModels { get; }

        IEnumerable<object> IRawModels.RawModels => RawModels;

        protected override async IAsyncEnumerable<TModel> UpdateAsSinglyOperationAsync(IDbConnection dbConnection)
        {
            foreach (TModel rawModel in RawModels)
            {
                yield return rawModel;
            }
        }

        protected override Task<IEnumerable<TModel>> UpdateOperationAsync(IDbConnection dbConnection)
        {
            throw new NotImplementedException();
        }
    }
}