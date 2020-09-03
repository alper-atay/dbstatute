using Basiclog;
using DbStatute.Extensions;
using DbStatute.Fundamentals.Multiples;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Multiples;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Internals;
using DbStatute.Qualifiers;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
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
            int selectedCount = RawModels.Count();

            if (selectedCount > 0)
            {
                bool fieldsBuilt = FieldQualifier.Build<TModel>(out IEnumerable<Field> fields);

                if (!fieldsBuilt)
                {
                    fields = null;
                }

                foreach (TModel rawModel in RawModels)
                {
                    if (fieldsBuilt)
                    {
                        IReadOnlyLogbook predicateLogs = RawModelHelper.PredicateModel(rawModel, fields, PredicateFieldQualifier);
                        Logs.AddRange(predicateLogs);

                        if (!predicateLogs.Safely)
                        {
                            continue;
                        }
                    }

                    TModel insertedModel = await dbConnection.InsertAsync<TModel, TModel>(rawModel, fields, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);

                    yield return insertedModel;
                }
            }
        }

        protected override async Task<IEnumerable<TModel>> InsertOperationAsync(IDbConnection dbConnection)
        {
            int selectedCount = RawModels.Count();

            if (selectedCount > 0)
            {
                bool fieldsBuilt = FieldQualifier.Build<TModel>(out IEnumerable<Field> fields);

                if (!fieldsBuilt)
                {
                    fields = null;
                }

                ICollection<TModel> insertModels = new Collection<TModel>();

                foreach (TModel rawModel in RawModels)
                {
                    if (fieldsBuilt)
                    {
                        IReadOnlyLogbook predicateLogs = RawModelHelper.PredicateModel(rawModel, fields, PredicateFieldQualifier);
                        Logs.AddRange(predicateLogs);

                        if (!predicateLogs.Safely)
                        {
                            continue;
                        }
                    }

                    insertModels.Add(rawModel);
                }

                if (insertModels.Count > 0)
                {
                    int insertedCount = await dbConnection.InsertAllAsync(insertModels, BatchSize, fields, Hints, CommandTimeout, Transaction);

                    if(insertedCount > 0)
                    {
                        return insertModels;
                    }

                    return insertedCount > 0 ? insertModels : null;
                }
            }

            return null;
        }
    }
}