using Basiclog;
using RepoDb.Interfaces;
using System;
using System.Data;

namespace DbStatute.Interfaces
{
    public interface IStatute
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

    public interface IStatute<TModel> : IStatute
        where TModel : class, IModel, new()
    {
        Type ModelType { get; }
    }
}