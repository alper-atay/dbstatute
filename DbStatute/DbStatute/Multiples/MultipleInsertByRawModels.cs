using Basiclog;
using DbStatute.Fundamentals.Multiples;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Multiples;
using DbStatute.Interfaces.Querying.Builders;
using DbStatute.Interfaces.Querying.Qualifiers.Fields;
using DbStatute.Internals;
using DbStatute.Querying.Builders;
using DbStatute.Querying.Qualifiers;
using DbStatute.Querying.Qualifiers.Fields;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Multiples
{
    public class MultipleInsertByRawModels<TModel> : MultipleInsertBase<TModel>, IMultipleInsertByRawModels<TModel>
        where TModel : class, IModel, new()
    {
        public MultipleInsertByRawModels(IEnumerable<TModel> rawModels)
        {
            RawModels = rawModels ?? throw new ArgumentNullException(nameof(rawModels));
            FieldQualifier = new FieldQualifier<TModel>();
            PredicateFieldQualifier = new PredicateFieldQualifier<TModel>();
        }

        public MultipleInsertByRawModels(IEnumerable<TModel> rawModels, IFieldQualifier<TModel> fieldQualifier, IPredicateFieldQualifier<TModel> predicateFieldQualifier)
        {
            RawModels = rawModels ?? throw new ArgumentNullException(nameof(rawModels));
            FieldQualifier = fieldQualifier ?? throw new ArgumentNullException(nameof(fieldQualifier));
            PredicateFieldQualifier = predicateFieldQualifier ?? throw new ArgumentNullException(nameof(predicateFieldQualifier));
        }

        public IFieldQualifier<TModel> FieldQualifier { get; }
        IFieldQualifier IMultipleInsertByRawModels.FieldQualifier => FieldQualifier;
        public IPredicateFieldQualifier<TModel> PredicateFieldQualifier { get; }
        IPredicateFieldQualifier IMultipleInsertByRawModels.PredicateFieldQualifier => PredicateFieldQualifier;
        public IEnumerable<TModel> RawModels { get; }
        IEnumerable<object> IRawModels.RawModels => RawModels;

        protected override async IAsyncEnumerable<TModel> InsertAsSingleOperationAsync(IDbConnection dbConnection)
        {
            IFieldBuilder<TModel> fieldBuilder = new FieldBuilder<TModel>(FieldQualifier);

            if (fieldBuilder.Build(out IEnumerable<Field> fields))
            {
                foreach (TModel rawModel in RawModels)
                {
                    IReadOnlyLogbook rawModelPredicateLogs = RawModelHelper.PredicateModel(rawModel, fields, PredicateFieldQualifier.ReadOnlyFieldPredicateMap);

                    Logs.AddRange(rawModelPredicateLogs);

                    if (!rawModelPredicateLogs.Safely)
                    {
                        continue;
                    }

                    TModel insertedModel = await dbConnection.InsertAsync<TModel, TModel>(rawModel, fields, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

                    yield return insertedModel;
                }
            }

            Logs.AddRange(fieldBuilder.ReadOnlyLogs);
        }

        protected override async Task<IEnumerable<TModel>> InsertOperationAsync(IDbConnection dbConnection)
        {
            IFieldBuilder<TModel> fieldBuilder = new FieldBuilder<TModel>(FieldQualifier);

            if (fieldBuilder.Build(out IEnumerable<Field> fields))
            {
                ICollection<TModel> readyModels = new Collection<TModel>();

                foreach (TModel rawModel in RawModels)
                {
                    IReadOnlyLogbook rawModelPredicateLogs = RawModelHelper.PredicateModel(rawModel, fields, PredicateFieldQualifier.ReadOnlyFieldPredicateMap);

                    Logs.AddRange(rawModelPredicateLogs);

                    if (!rawModelPredicateLogs.Safely)
                    {
                        continue;
                    }

                    readyModels.Add(rawModel);
                }

                if (readyModels.Count > 0)
                {
                    int insertedCount = await dbConnection.InsertAllAsync(readyModels, BatchSize, fields, Hints, CommandTimeout, Transaction);

                    return insertedCount > 0 ? readyModels : null;
                }
            }

            Logs.AddRange(fieldBuilder.ReadOnlyLogs);

            return null;
        }
    }
}