using DbStatute.Fundamentals.Multiples;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Multiples;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Multiples
{
    public class MultipleUpdateByRawModels<TModel> : MultipleUpdateBase<TModel>, IMultipleUpdateBySourceModels<TModel>
            where TModel : class, IModel, new()
    {
        public MultipleUpdateByRawModels(IEnumerable<TModel> rawModels)
        {
            SourceModels = rawModels ?? throw new ArgumentNullException(nameof(rawModels));
        }

        public IEnumerable<TModel> SourceModels { get; }

        IEnumerable<object> ISourceModels.SourceModels => SourceModels;

        protected override async IAsyncEnumerable<TModel> UpdateAsSinglyOperationAsync(IDbConnection dbConnection)
        {
            foreach (TModel rawModel in SourceModels)
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