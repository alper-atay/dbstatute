using Basiclog;
using DbStatute.Enumerations;
using DbStatute.Extensions;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals;
using DbStatute.Interfaces.Fundamentals.Queries;
using DbStatute.Interfaces.Queries;
using RepoDb;
using RepoDb.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DbStatute.Fundamentals
{
    public abstract class StatuteBase : IStatuteBase
    {
        private StatuteResult _statuteResult = StatuteResult.Unknown;

        public IReadOnlyLogbook ReadOnlyLogs => Logs;

        protected ILogbook Logs { get; } = Logger.NewLogbook();

        #region IStatute

        public event Action Failed;

        public event Action Succeed;

        public ICacheable Cacheable { get; set; }

        public int? CommandTimeout { get; set; } = null;

        public string Hints { get; set; } = null;

        public bool IsFailed => StatuteResult == StatuteResult.Failure;

        public bool IsSucceed => StatuteResult == StatuteResult.Success;

        public IStatementBuilder StatementBuilder { get; set; } = null;

        public StatuteResult StatuteResult
        {
            get => _statuteResult;
            set
            {
                _statuteResult = value;

                switch (_statuteResult)
                {
                    case StatuteResult.Failure:
                        {
                            Failed?.Invoke();
                            break;
                        }
                    case StatuteResult.Success:
                        {
                            Succeed?.Invoke();
                            break;
                        }
                    case StatuteResult.Unknown:
                        {
                            break;
                        }
                }
            }
        }

        public ITrace Trace { get; set; } = null;

        public IDbTransaction Transaction { get; set; } = null;

        #endregion IStatute

        [Conditional("DEBUG")]
        protected static void ThrowIfNotNull(object obj, string objName)
        {
            if (!(obj is null))
            {
                throw new NoNullAllowedException($"{objName} must be null");
            }
        }
    }

    public abstract class StatuteBase<TModel> : StatuteBase, IStatuteBase<TModel>
        where TModel : class, IModel, new()
    {
        public Type ModelType => typeof(TModel);

        protected IEnumerable<Field> GetFieldableQueryResult(IFieldableQuery<TModel> fieldableQuery)
        {
            bool fieldsBuilt = fieldableQuery.FieldQuery.Fields.Build(out IEnumerable<Field> fields);

            return fieldsBuilt ? fields : null;
        }

        protected TModel GetModelableQueryResult(IModelableQuery<TModel> modelableQuery)
        {
            Logs.AddRange(modelableQuery.ModelQuery.Build(out TModel model));

            if (ReadOnlyLogs.Safely)
            {
                return model;
            }

            return null;
        }

        protected Task<TModel> GetModelAsync(IDbConnection dbConnection, object what, IEnumerable<Field> fields = null, IEnumerable<OrderField> orderFields = null)
        {
            return dbConnection.QueryAsync<TModel>(what, fields, orderFields, 1, Hints, Cacheable?.Key, Cacheable?.ItemExpiration, CommandTimeout, Transaction, Cacheable?.Cache, Trace, StatementBuilder).ContinueWith(x => x.Result.FirstOrDefault());
        }

        protected Task<TModel> GetModelAsync(IDbConnection dbConnection, QueryGroup queryGroup, IEnumerable<Field> fields = null, IEnumerable<OrderField> orderFields = null)
        {
            return dbConnection.QueryAsync<TModel>(queryGroup, fields, orderFields, 1, Hints, Cacheable?.Key, Cacheable?.ItemExpiration, CommandTimeout, Transaction, Cacheable?.Cache, Trace, StatementBuilder).ContinueWith(x => x.Result.FirstOrDefault());
        }

        protected IEnumerable<OrderField> GetOrderFieldableQueryResult(IOrderFieldableQuery<TModel> orderFieldableQuery)
        {
            bool orderFieldsBuilt = orderFieldableQuery.OrderFieldQuery.OrderFields.Build(out IEnumerable<OrderField> orderFields);

            return orderFieldsBuilt ? orderFields : null;
        }

        protected QueryGroup GetSearchableProxyResult(ISearchableQuery<TModel> searchableQuery)
        {
            Logs.AddRange(searchableQuery.SearchQuery.Build(searchableQuery.Conjunction, out QueryGroup queryGroup));

            if (ReadOnlyLogs.Safely)
            {
                return queryGroup;
            }

            return null;
        }

        protected TModel GetSourceableModelQueryResult(ISourceableQuery<TModel> sourceableModelQuery)
        {
            return sourceableModelQuery.SourceModel;
        }
    }
}