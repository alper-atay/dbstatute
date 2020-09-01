using DbStatute.Interfaces;
using DbStatute.Interfaces.Qualifiers;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DbStatute.Qualifiers
{
    public class FieldQualifier : IFieldQualifier
    {
        public FieldQualifier()
        {
            FieldHashSet = new HashSet<Field>();
        }

        public FieldQualifier(IEnumerable<Field> fields)
        {
            if (fields is null)
            {
                throw new ArgumentNullException(nameof(fields));
            }

            FieldHashSet = new HashSet<Field>(fields);
        }

        public bool HasField => FieldHashSet.Count > 0;
        public IEnumerable<Field> Fields => FieldHashSet;
        protected HashSet<Field> FieldHashSet { get; }

        public IEnumerable<Field> GetAllByName(string name)
        {
            return FieldHashSet.Where(x => x.Name == name);
        }

        public IEnumerable<Field> GetAllByType(Type type)
        {
            return FieldHashSet.Where(x => x.Type == type);
        }

        public IEnumerable<Field> GetAllByType<T>()
        {
            Type type = typeof(T);

            return GetAllByType(type);
        }

        public bool IsSetted(Field field)
        {
            return FieldHashSet.Contains(field);
        }

        public int IsSetted(string name)
        {
            return FieldHashSet.Count(x => x.Name == name);
        }

        public bool Set(Field field, bool overrideEnabled = false)
        {
            if (!FieldHashSet.Add(field))
            {
                if (overrideEnabled)
                {
                    FieldHashSet.Remove(field);
                    return FieldHashSet.Add(field);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        public bool Unset(Field field)
        {
            return FieldHashSet.Remove(field);
        }
    }

    public class FieldQualifier<TModel> : FieldQualifier, IFieldQualifier<TModel>
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

        public int Set(Expression<Func<TModel, object>> expression, bool overrideEnabled = false)
        {
            IEnumerable<Field> fields = Field.Parse(expression);

            int settedCount = 0;

            foreach (Field field in fields)
            {
                if (Set(field, overrideEnabled))
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