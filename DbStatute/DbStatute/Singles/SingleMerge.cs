using DbStatute.Fundamentals;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying.Qualifiers;
using DbStatute.Querying.Qualifiers;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DbStatute
{
    public abstract class SingleMerge<TModel> : MergeBase<TModel>, ISingleMerge<TModel>

        where TModel : class, IModel, new()
    {
        private TModel _mergedModel;

        protected SingleMerge()
        {
            ModelQueryQualifier = new ModelQueryQualifier<TModel>();
        }

        protected SingleMerge(IModelQueryQualifier<TModel> modelQueryQualifier)
        {
            ModelQueryQualifier = modelQueryQualifier ?? throw new ArgumentNullException(nameof(modelQueryQualifier));
        }

        public override int MergedCount => _mergedModel is null ? 0 : 1;
        public TModel MergedModel => (TModel)_mergedModel?.Clone();
        object ISingleMerge.MergedModel => MergedModel;
        public IModelQueryQualifier<TModel> ModelQueryQualifier { get; }
        IModelQueryQualifier ISingleMerge.ModelQueryQualifier => ModelQueryQualifier;

        public async Task<TModel> MergeAsync(IDbConnection dbConnection)
        {
            _mergedModel = null;

            if (ReadOnlyLogs.Safely)
            {
                _mergedModel = await MergeOperationAsync(dbConnection);
            }

            StatuteResult = _mergedModel is null ? StatuteResult.Failure : StatuteResult.Success;

            return MergedModel;
        }

        Task<object> ISingleMerge.MergeAsync(IDbConnection dbConnection)
        {
            return MergeAsync(dbConnection).ContinueWith(x => (object)x.Result);
        }

        protected virtual async Task<TModel> MergeOperationAsync(IDbConnection dbConnection)
        {
            Logs.AddRange(ModelQueryQualifier.GetModel(out TModel rawModel));

            if (!Logs.Safely)
            {
                return null;
            }

            IFieldQualifier<TModel> fieldQualifier = ModelQueryQualifier.FieldQualifier;

            IEnumerable<Field> fields = null;
            if (fieldQualifier.HasField)
            {
                fields = fieldQualifier.Fields;
            }

            object mergedModelId = await dbConnection.MergeAsync<TModel, object>(rawModel, fields, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);
            return await dbConnection.QueryAsync<TModel>(mergedModelId, null, 1).ContinueWith(x => x.Result.FirstOrDefault());
        }
    }
}