using DbStatute.Interfaces;
using DbStatute.Interfaces.Qualifiers;
using RepoDb;
using RepoDb.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DbStatute.Qualifiers
{
    public class OperationFieldQualifier : IOperationFieldQualifier
    {
        public IReadOnlyDictionary<Field, Operation> FieldOperationPairs => FieldOperationDictionary;
        protected Dictionary<Field, Operation> FieldOperationDictionary { get; } = new Dictionary<Field, Operation>();

        public IEnumerable<Field> GetAllByName(string name)
        {
            Dictionary<Field, Operation>.KeyCollection fields = FieldOperationDictionary.Keys;

            return fields.Where(x => x.Name.Equals(name));
        }

        public IEnumerable<Field> GetAllByType(Type type)
        {
            Dictionary<Field, Operation>.KeyCollection fields = FieldOperationDictionary.Keys;

            return fields.Where(x => x.Type.Equals(type));
        }

        public IEnumerable<Field> GetAllByType<T>()
        {
            Type type = typeof(T);

            return GetAllByType(type);
        }

        public bool IsSetted(Field field)
        {
            Dictionary<Field, Operation>.KeyCollection fields = FieldOperationDictionary.Keys;

            return fields.Contains(field);
        }

        public int IsSetted(string name)
        {
            Dictionary<Field, Operation>.KeyCollection fields = FieldOperationDictionary.Keys;

            return fields.Count(x => x.Name.Equals(name));
        }

        public bool Set(Field field, Operation value, bool overrideEnabled = false)
        {
            if (!FieldOperationDictionary.TryAdd(field, value))
            {
                if (overrideEnabled)
                {
                    FieldOperationDictionary.Remove(field);

                    return FieldOperationDictionary.TryAdd(field, value);
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

            if (!FieldOperationDictionary.TryAdd(field, value))
            {
                if (overrideEnabled)
                {
                    FieldOperationDictionary.Remove(field);

                    return FieldOperationDictionary.TryAdd(field, value);
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
            return FieldOperationDictionary.Remove(field);
        }
    }

    public class OperationFieldQualifier<TModel> : OperationFieldQualifier, IOperationFieldQualifier<TModel>
        where TModel : class, IModel, new()
    {
        public int IsSetted(Expression<Func<TModel, object>> expression)
        {
            IEnumerable<Field> fields = Field.Parse(expression);
            int settedCount = 0;

            foreach (Field field in fields)
            {
                if (IsSetted(field))
                {
                    settedCount += 1;
                }
            }

            return settedCount;
        }

        public int Set(Expression<Func<TModel, object>> expression, Operation value, bool overrideEnabled = false)
        {
            IEnumerable<Field> fields = Field.Parse(expression);

            int settedCount = 0;

            foreach (Field field in fields)
            {
                if (Set(field, value, overrideEnabled))
                {
                    settedCount += 1;
                }
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
                if (Set(field, value, overrideEnabled))
                {
                    settedCount += 1;
                }
            }

            return settedCount;
        }

        public int Unset(Expression<Func<TModel, object>> expression)
        {
            IEnumerable<Field> fields = Field.Parse(expression);

            int unsettedCount = 0;

            foreach (Field field in fields)
            {
                if (Unset(field))
                {
                    unsettedCount += 1;
                }
            }

            return unsettedCount;
        }
    }
}