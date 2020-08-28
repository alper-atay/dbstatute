using DbStatute.Interfaces;
using DbStatute.Interfaces.Querying.Qualifiers;
using RepoDb;
using RepoDb.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DbStatute.Querying.Qualifiers.Fields
{
    public class OperationFieldQualifier<TModel> : IOperationFieldQualifier<TModel>
        where TModel : class, IModel, new()
    {
        private readonly Dictionary<Field, Operation> _operationMap = new Dictionary<Field, Operation>();

        public OperationFieldQualifier()
        {
            FieldQualifier = new FieldQualifier<TModel>();
        }

        public OperationFieldQualifier(IFieldQualifier<TModel> fieldQualifier)
        {
            FieldQualifier = fieldQualifier ?? throw new ArgumentNullException(nameof(fieldQualifier));
        }

        public IReadOnlyDictionary<Field, Operation> FieldOperationMap => _operationMap;
        public IFieldQualifier<TModel> FieldQualifier { get; }
        IFieldQualifier IOperationFieldQualifier.FieldQualifier => FieldQualifier;

        public bool IsSetted(Expression<Func<TModel, object>> expression)
        {
            IEnumerable<Field> fields = Field.Parse(expression);

            bool isSetted = fields.Count() > 0;

            foreach (Field field in fields)
            {
                isSetted = isSetted && _operationMap.ContainsKey(field);
            }

            return isSetted;
        }

        public bool IsSetted(Field field)
        {
            return _operationMap.ContainsKey(field);
        }

        public bool Set(Expression<Func<TModel, object>> expression, Operation value, bool overrideEnabled = false)
        {
            IEnumerable<Field> fields = Field.Parse(expression);

            int setCount = 0;

            foreach (Field field in fields)
            {
                if (!_operationMap.TryAdd(field, value))
                {
                    if (overrideEnabled)
                    {
                        _operationMap[field] = value;
                    }
                    else
                    {
                        continue;
                    }
                }

                setCount += 1;
            }

            return setCount > 0;
        }

        public bool Set(Field field, Operation value, bool overrideEnabled = false)
        {
            if (!_operationMap.TryAdd(field, value))
            {
                if (overrideEnabled)
                {
                    _operationMap[field] = value;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public bool Set(Expression<Func<TModel, object>> expression, bool overrideEnabled = false)
        {
            throw new NotImplementedException();
        }

        public bool Set(Field field, bool overrideEnabled = false)
        {
            throw new NotImplementedException();
        }

        public bool Unset(Expression<Func<TModel, object>> expression)
        {
            IEnumerable<Field> fields = Field.Parse(expression);

            int unsetCount = 0;

            foreach (Field field in fields)
            {
                if (_operationMap.Remove(field))
                {
                    unsetCount += 1;
                }
            }

            return unsetCount > 0;
        }

        public bool Unset(Field field)
        {
            return _operationMap.Remove(field);
        }
    }
}