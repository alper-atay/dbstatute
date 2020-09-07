using Basiclog;
using DbStatute.Extensions;
using DbStatute.Fundamentals.Multiples;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Multiples;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Qualifiers;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Multiples
{
    public class MultipleMergeByRawModels<TModel> : MultipleMergeBase<TModel>, IMultipleMergeBySourceModels<TModel>
        where TModel : class, IModel, new()
    {
        public MultipleMergeByRawModels(IEnumerable<TModel> rawModels)
        {
            SourceModels = rawModels ?? throw new ArgumentNullException(nameof(rawModels));
            FieldQualifier = new FieldQualifier<TModel>();
            PredicateFieldQualifier = new PredicateFieldQualifier<TModel>();
        }

        public MultipleMergeByRawModels(IEnumerable<TModel> rawModels, IFieldQualifier<TModel> fieldQualifier, IPredicateFieldQualifier<TModel> predicateFieldQualifier)
        {
            SourceModels = rawModels ?? throw new ArgumentNullException(nameof(rawModels));
            FieldQualifier = fieldQualifier ?? throw new ArgumentNullException(nameof(fieldQualifier));
            PredicateFieldQualifier = predicateFieldQualifier ?? throw new ArgumentNullException(nameof(predicateFieldQualifier));
        }

        public IFieldQualifier<TModel> FieldQualifier { get; }

        IFieldQualifier IMultipleMergeBySourceModels.FieldQualifier => FieldQualifier;

        public IPredicateFieldQualifier<TModel> PredicateFieldQualifier { get; }

        IPredicateFieldQualifier IMultipleMergeBySourceModels.PredicateFieldQualifier => PredicateFieldQualifier;

        public IEnumerable<TModel> SourceModels { get; }

        IEnumerable<object> ISourceModels.SourceModels => SourceModels;

        protected override async IAsyncEnumerable<TModel> MergeAsSinglyOperationAsync(IDbConnection dbConnection)
        {
            bool fieldQualified = FieldQualifier.Build<TModel>(out IEnumerable<Field> fields);

            if (!fieldQualified)
            {
                fields = null;
            }

            foreach (TModel rawModel in SourceModels)
            {
                IReadOnlyLogbook rawModelPredicateLogs = rawModel.Predicate(fields, PredicateFieldQualifier);

                Logs.AddRange(rawModelPredicateLogs);

                if (!rawModelPredicateLogs.Safely)
                {
                    continue;
                }

                TModel mergedModel = await dbConnection.MergeAsync<TModel, TModel>(rawModel, fields, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

                yield return mergedModel;
            }
        }

        protected override async Task<IEnumerable<TModel>> MergeOperationAsync(IDbConnection dbConnection)
        {
            bool fieldsBuilt = FieldQualifier.Build<TModel>(out IEnumerable<Field> fields);

            if (!fieldsBuilt)
            {
                fields = null;
            }

            ICollection<TModel> mergeModels = new Collection<TModel>();

            foreach (TModel rawModel in SourceModels)
            {
                IReadOnlyLogbook predicateLogs = rawModel.Predicate(fields, PredicateFieldQualifier);

                Logs.AddRange(predicateLogs);

                if (!predicateLogs.Safely)
                {
                    continue;
                }

                mergeModels.Add(rawModel);
            }

            if (mergeModels.Count > 0)
            {
                int mergedCount = await dbConnection.MergeAllAsync(mergeModels, BatchSize, fields, Hints, CommandTimeout, Transaction);

                if (mergedCount > 0)
                {
                    return mergeModels;
                }
            }

            return null;
        }
    }
}