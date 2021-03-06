﻿using DbStatute.Interfaces;
using DbStatute.Interfaces.Qualifiers;
using DbStatute.Interfaces.Queries;
using DbStatute.Qualifiers;
using System;

namespace DbStatute.Queries
{
    public class ModelQuery : IModelQuery
    {
        public ModelQuery()
        {
            Fields = new FieldQualifier();
            ValueMap = new ValueFieldQualifier();
            PredicateMap = new PredicateFieldQualifier();
        }

        public ModelQuery(IFieldQualifier fields, IValueFieldQualifier valueMap, IPredicateFieldQualifier predicateMap)
        {
            Fields = fields ?? throw new ArgumentNullException(nameof(fields));
            ValueMap = valueMap ?? throw new ArgumentNullException(nameof(valueMap));
            PredicateMap = predicateMap ?? throw new ArgumentNullException(nameof(predicateMap));
        }

        public IFieldQualifier Fields { get; }

        public IPredicateFieldQualifier PredicateMap { get; }

        public IValueFieldQualifier ValueMap { get; }
    }

    public class ModelQuery<TModel> : IModelQuery<TModel>
        where TModel : class, IModel, new()
    {
        public ModelQuery()
        {
            Fields = new FieldQualifier<TModel>();
            ValueMap = new ValueFieldQualifier<TModel>();
            PredicateMap = new PredicateFieldQualifier<TModel>();
        }

        public ModelQuery(IFieldQualifier<TModel> fields, IValueFieldQualifier<TModel> valueMap, IPredicateFieldQualifier<TModel> predicateMap)
        {
            Fields = fields ?? throw new ArgumentNullException(nameof(fields));
            ValueMap = valueMap ?? throw new ArgumentNullException(nameof(valueMap));
            PredicateMap = predicateMap ?? throw new ArgumentNullException(nameof(predicateMap));
        }

        public IFieldQualifier<TModel> Fields { get; }

        IFieldQualifier IModelQuery.Fields => Fields;

        public IPredicateFieldQualifier<TModel> PredicateMap { get; }

        IPredicateFieldQualifier IModelQuery.PredicateMap => PredicateMap;

        public IValueFieldQualifier<TModel> ValueMap { get; }

        IValueFieldQualifier IModelQuery.ValueMap => ValueMap;
    }
}