using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying.Qualifiers.Fields;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DbStatute.Querying.Qualifiers
{
    public class FieldQualifier : IFieldQualifier
    {
        public FieldQualifier()
        {
            Fields = new HashSet<Field>();
        }

        public FieldQualifier(IEnumerable<Field> fields)
        {
            if (fields is null)
            {
                throw new ArgumentNullException(nameof(fields));
            }

            Fields = new HashSet<Field>(fields);
        }

        public bool HasField => Fields.Count > 0;
        public IEnumerable<Field> ReadOnlyFields => Fields;
        protected HashSet<Field> Fields { get; }

        public IEnumerable<Field> GetAllByName(string name)
        {
            return Fields.Where(x => x.Name == name);
        }

        public IEnumerable<Field> GetAllByType(Type type)
        {
            return Fields.Where(x => x.Type == type);
        }

        public IEnumerable<Field> GetAllByType<T>()
        {
            Type type = typeof(T);

            return GetAllByType(type);
        }

        public bool IsSetted(Field field)
        {
            return Fields.Contains(field);
        }

        public int IsSetted(string name)
        {
            return Fields.Count(x => x.Name == name);
        }

        public bool Set(Field field, bool overrideEnabled = false)
        {
            if (!Fields.Add(field))
            {
                if (overrideEnabled)
                {
                    Fields.Remove(field);
                    return Fields.Add(field);
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
            return Fields.Remove(field);
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