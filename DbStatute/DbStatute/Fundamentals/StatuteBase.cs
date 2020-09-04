using Basiclog;
using DbStatute.Enumerations;
using DbStatute.Interfaces;
using DbStatute.Interfaces.Fundamentals;
using RepoDb.Interfaces;
using System;
using System.Data;

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
    }

    public abstract class StatuteBase<TModel> : StatuteBase, IStatuteBase<TModel>
        where TModel : class, IModel, new()
    {
        public Type ModelType => typeof(TModel);
    }
}