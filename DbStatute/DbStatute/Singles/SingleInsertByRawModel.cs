﻿using DbStatute.Fundamentals.Singles;
using DbStatute.Helpers;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Interfaces.Singles;
using DbStatute.Qualifiers;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DbStatute.Singles
{
    public class SingleInsertByRawModel<TModel> : SingleInsertBase<TModel>, ISingleInsertByRawModel<TModel>
        where TModel : class, IModel, new()
    {
        public SingleInsertByRawModel(TModel rawModel)
        {
            RawModel = rawModel ?? throw new ArgumentNullException(nameof(rawModel));
            FieldQualifier = new FieldQualifier<TModel>();
            PredicateFieldQualifier = new PredicateFieldQualifier<TModel>();
        }

        public SingleInsertByRawModel(TModel rawModel, IFieldQualifier<TModel> fieldQualifier)
        {
            RawModel = rawModel ?? throw new ArgumentNullException(nameof(rawModel));
            FieldQualifier = fieldQualifier ?? throw new ArgumentNullException(nameof(fieldQualifier));
            PredicateFieldQualifier = new PredicateFieldQualifier<TModel>();
        }

        public SingleInsertByRawModel(TModel rawModel, IPredicateFieldQualifier<TModel> predicateFieldQualifier)
        {
            RawModel = rawModel ?? throw new ArgumentNullException(nameof(rawModel));
            FieldQualifier = new FieldQualifier<TModel>();
            PredicateFieldQualifier = predicateFieldQualifier ?? throw new ArgumentNullException(nameof(predicateFieldQualifier));
        }

        public SingleInsertByRawModel(TModel rawModel, IFieldQualifier<TModel> fieldQualifier, IPredicateFieldQualifier<TModel> predicateFieldQualifier)
        {
            RawModel = rawModel ?? throw new ArgumentNullException(nameof(rawModel));
            FieldQualifier = fieldQualifier ?? throw new ArgumentNullException(nameof(fieldQualifier));
            PredicateFieldQualifier = predicateFieldQualifier ?? throw new ArgumentNullException(nameof(predicateFieldQualifier));
        }

        public IFieldQualifier<TModel> FieldQualifier { get; }

        IFieldQualifier ISingleInsertByRawModel.FieldQualifier => FieldQualifier;

        public IPredicateFieldQualifier<TModel> PredicateFieldQualifier { get; }

        IPredicateFieldQualifier ISingleInsertByRawModel.PredicateFieldQualifier => PredicateFieldQualifier;

        public TModel RawModel { get; }

        object IRawModel.RawModel => RawModel;

        protected override async Task<TModel> InsertOperationAsync(IDbConnection dbConnection)
        {
            Logs.AddRange(RawModelHelper.PredicateModel(RawModel, FieldQualifier, PredicateFieldQualifier, out IEnumerable<Field> fields));

            if (ReadOnlyLogs.Safely)
            {
                return await dbConnection.InsertAsync<TModel, TModel>(RawModel, fields, Hints, CommandTimeout, Transaction, Trace, StatementBuilder);
            }

            return null;
        }
    }
}