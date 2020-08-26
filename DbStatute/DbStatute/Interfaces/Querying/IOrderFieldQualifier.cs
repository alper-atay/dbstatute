using DbStatute.Querying;
using RepoDb;
using RepoDb.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DbStatute.Interfaces.Querying
{
    public interface IOrderFieldQualifier
    {
        private static readonly IOrderFieldQualifier _empty;

        static IOrderFieldQualifier()
        {
            _empty = new OrderFieldQualifier(Enumerable.Empty<OrderField>());
        }

        static IOrderFieldQualifier Empty => _empty;

        IEnumerable<OrderField> OrderFields { get; }
    }

    public interface IOrderFieldQualifier<TModel> : IOrderFieldQualifier
        where TModel : class, IModel, new()
    {
        bool IsOrderFieldSetted(Expression<Func<TModel, object>> property);

        bool IsOrderFieldSetted(string propertyName);

        bool SetOrderField(Expression<Func<TModel, object>> property, Order order, bool overrideEnabled = false);

        bool SetOrderField(string propertyName, Order order, bool overrideEnabled = false);

        bool UnsetOrderField(Expression<Func<TModel, object>> property);

        bool UnsetOrderField(string propertyName);
    }
}