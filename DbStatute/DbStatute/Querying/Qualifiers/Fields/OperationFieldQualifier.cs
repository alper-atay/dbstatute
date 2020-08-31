﻿using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying.Qualifiers.Fields;
using RepoDb;
using RepoDb.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DbStatute.Querying.Qualifiers.Fields
{
    public class OperationFieldQualifier : IOperationFieldQualifier
    {
        public IReadOnlyDictionary<Field, Operation> ReadOnlyFieldOperationMap => FieldOperationMap;
        protected Dictionary<Field, Operation> FieldOperationMap { get; } = new Dictionary<Field, Operation>();

        public IEnumerable<Field> GetAllByName(string name)
        {
            Dictionary<Field, Operation>.KeyCollection fields = FieldOperationMap.Keys;

            return fields.Where(x => x.Name.Equals(name));
        }

        public IEnumerable<Field> GetAllByType(Type type)
        {
            Dictionary<Field, Operation>.KeyCollection fields = FieldOperationMap.Keys;

            return fields.Where(x => x.Type.Equals(type));
        }

        public IEnumerable<Field> GetAllByType<T>()
        {
            Type type = typeof(T);

            return GetAllByType(type);
        }

        public bool IsSetted(Field field)
        {
            Dictionary<Field, Operation>.KeyCollection fields = FieldOperationMap.Keys;

            return fields.Contains(field);
        }

        public int IsSetted(string name)
        {
            Dictionary<Field, Operation>.KeyCollection fields = FieldOperationMap.Keys;

            return fields.Count(x => x.Name.Equals(name));
        }

        public bool Set(Field field, Operation value, bool overrideEnabled = false)
        {
            if (!FieldOperationMap.TryAdd(field, value))
            {
                if (overrideEnabled)
                {
                    FieldOperationMap.Remove(field);

                    return FieldOperationMap.TryAdd(field, value);
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public bool Set(Field field, bool overrideEnabled = false)
        {
            const Operation value = Operation.Equal;

            if (!FieldOperationMap.TryAdd(field, value))
            {
                if (overrideEnabled)
                {
                    FieldOperationMap.Remove(field);

                    return FieldOperationMap.TryAdd(field, value);
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public bool Unset(Field field)
        {
            return FieldOperationMap.Remove(field);
        }
    }

    public class OperationFieldQualifier<TModel> : OperationFieldQualifier, IOperationFieldQualifier<TModel>
        where TModel : class, IModel, new()
    {
        public bool IsSetted(Expression<Func<TModel, object>> expression)
        {
            IEnumerable<Field> fields = Field.Parse(expression);

            bool isSetted = fields.Count() > 0;

            foreach (Field field in fields)
            {
                isSetted = isSetted && FieldOperationMap.ContainsKey(field);
            }

            return isSetted;
        }

        public int Set(Expression<Func<TModel, object>> expression, Operation value, bool overrideEnabled = false)
        {
            IEnumerable<Field> fields = Field.Parse(expression);

            int settedCount = 0;

            foreach (Field field in fields)
            {
                if (!FieldOperationMap.TryAdd(field, value))
                {
                    if (overrideEnabled)
                    {
                        FieldOperationMap.Remove(field);

                        if (!FieldOperationMap.TryAdd(field, default))
                        {
                            continue;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }

                settedCount += 1;
            }

            return settedCount;
        }

        public int Set(Expression<Func<TModel, object>> expression, bool overrideEnabled = false)
        {
            const Operation value = Operation.Equal;

            IEnumerable<Field> fields = Field.Parse(expression);

            int settedCount = 0;

            foreach (Field field in fields)
            {
                if (!FieldOperationMap.TryAdd(field, value))
                {
                    if (overrideEnabled)
                    {
                        FieldOperationMap.Remove(field);

                        if (!FieldOperationMap.TryAdd(field, value))
                        {
                            continue;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }

                settedCount += 1;
            }

            return settedCount;
        }

        public int Unset(Expression<Func<TModel, object>> expression)
        {
            IEnumerable<Field> fields = Field.Parse(expression);

            int unsettedCount = 0;

            foreach (Field field in fields)
            {
                if (FieldOperationMap.Remove(field))
                {
                    unsettedCount += 1;
                }
            }

            return unsettedCount;
        }
    }
}