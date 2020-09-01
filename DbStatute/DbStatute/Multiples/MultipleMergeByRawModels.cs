using Basiclog;
using DbStatute.Builders;
using DbStatute.Fundamentals.Multiples;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Builders;
using DbStatute.Interfaces.Multiples;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Internals;
using DbStatute.Qualifiers;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Multiples
{
    public class MultipleMergeByRawModels<TModel> : MultipleMergeBase<TModel>, IMultipleMergeByRawModels<TModel>
        where TModel : class, IModel, new()
    {
        public MultipleMergeByRawModels(IEnumerable<TModel> rawModels)
        {
            RawModels = rawModels ?? throw new ArgumentNullException(nameof(rawModels));
            FieldQualifier = new FieldQualifier<TModel>();
            PredicateFieldQualifier = new PredicateFieldQualifier<TModel>();
        }

        public MultipleMergeByRawModels(IEnumerable<TModel> rawModels, IFieldQualifier<TModel> fieldQualifier, IPredicateFieldQualifier<TModel> predicateFieldQualifier)
        {
            RawModels = rawModels ?? throw new ArgumentNullException(nameof(rawModels));
            FieldQualifier = fieldQualifier ?? throw new ArgumentNullException(nameof(fieldQualifier));
            PredicateFieldQualifier = predicateFieldQualifier ?? throw new ArgumentNullException(nameof(predicateFieldQualifier));
        }

        public IFieldQualifier<TModel> FieldQualifier { get; }
        IFieldQualifier IMultipleMergeByRawModels.FieldQualifier => FieldQualifier;
        public IPredicateFieldQualifier<TModel> PredicateFieldQualifier { get; }
        IPredicateFieldQualifier IMultipleMergeByRawModels.PredicateFieldQualifier => PredicateFieldQualifier;
        public IEnumerable<TModel> RawModels { get; }
        IEnumerable<object> IRawModels.RawModels => RawModels;

        protected override async IAsyncEnumerable<TModel> MergeAsSinglyOperationAsync(IDbConnection dbConnection)
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

                    TModel mergedModel = await dbConnection.MergeAsync<TModel, TModel>(rawModel, fields, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

                    yield return mergedModel;
                }
            }

            Logs.AddRange(fieldBuilder.ReadOnlyLogs);
        }

        protected override async Task<IEnumerable<TModel>> MergeOperationAsync(IDbConnection dbConnection)
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
                    int mergedCount = await dbConnection.MergeAllAsync(readyModels, BatchSize, fields, Hints, CommandTimeout, Transaction);

                    return mergedCount > 0 ? readyModels : null;
                }
            }

            Logs.AddRange(fieldBuilder.ReadOnlyLogs);

            return null;
        }
    }
}