using DbStatute.Fundamentals.Singles;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Interfaces.Singles;
using DbStatute.Internals;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DbStatute.Singles
{
    public class SingleUpdateByRawModel<TModel, TFieldQualifier, TPredicateFieldQualifier> : SingleUpdateBase<TModel>, ISingleUpdateByRawModel<TModel, TFieldQualifier, TPredicateFieldQualifier>
        where TModel : class, IModel, new()
        where TFieldQualifier : class, IFieldQualifier<TModel>
        where TPredicateFieldQualifier : class, IPredicateFieldQualifier<TModel>
    {
        public SingleUpdateByRawModel(TModel rawModel)
        {
            RawModel = rawModel ?? throw new ArgumentNullException(nameof(rawModel));
        }

        public SingleUpdateByRawModel(TModel rawModel, TFieldQualifier fieldQualifier, TPredicateFieldQualifier predicateFieldQualifier)
        {
            RawModel = rawModel ?? throw new ArgumentNullException(nameof(rawModel));
            FieldQualifier = fieldQualifier ?? throw new ArgumentNullException(nameof(fieldQualifier));
            PredicateFieldQualifier = predicateFieldQualifier ?? throw new ArgumentNullException(nameof(predicateFieldQualifier));
        }

        public TFieldQualifier FieldQualifier { get; }

        public TPredicateFieldQualifier PredicateFieldQualifier { get; }

        public TModel RawModel { get; }

        object IRawModel.RawModel => RawModel;

        protected override async Task<TModel> UpdateOperationAsync(IDbConnection dbConnection)
        {
            Logs.AddRange(RawModelHelper.PredicateModel(RawModel, FieldQualifier, PredicateFieldQualifier, out IEnumerable<Field> fields));

            if (ReadOnlyLogs.Safely)
            {
                int updatedId = await dbConnection.UpdateAsync(RawModel, fields, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

                return await dbConnection.QueryAsync<TModel>(updatedId, null, null, 1, Hints, Cacheable?.Key, Cacheable?.ItemExpiration, CommandTimeout, Transaction, Cacheable?.Cache)
                    .ContinueWith(x => x.Result.FirstOrDefault());
            }

            return null;
        }
    }
}