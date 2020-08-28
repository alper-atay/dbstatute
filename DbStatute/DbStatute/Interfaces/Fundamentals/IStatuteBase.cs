using Basiclog;
using DbStatute.Enumerations;
using RepoDb.Interfaces;
using System;
using System.Data;

namespace DbStatute.Interfaces.Fundamentals
{
    public interface IStatuteBase
    {
        event Action Failed;

        event Action Succeed;

        int? CommandTimeout { get; set; }
        string Hints { get; set; }
        public IReadOnlyLogbook ReadOnlyLogs { get; }
        IStatementBuilder StatementBuilder { get; set; }
        StatuteResult StatuteResult { get; set; }
        ITrace Trace { get; set; }
        IDbTransaction Transaction { get; set; }
    }

    public interface IStatuteBase<TModel> : IStatuteBase
        where TModel : class, IModel, new()
    {
        Type ModelType { get; }
    }
}