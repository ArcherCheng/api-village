using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Api.Models;
///https://stackoverflow.com/questions/72128143/net-core-entity-framework-6-nested-transaction-architecture

// public class NestedTransactionManager : IDbContextTransactionManager
// {
//     readonly ISqlServerConnection _sqlServerConnection;

//     public NestedTransactionManager (ISqlServerConnection sqlServerConnection)
//     {
//         _sqlServerConnection = sqlServerConnection;
//     }

//     internal int Layer = 0;

//     public IDbContextTransaction CurrentTransaction => _sqlServerConnection.CurrentTransaction;

//     public IDbContextTransaction BeginTransaction()
//     {
//         if (Layer++ == 0) {
//             _sqlServerConnection.BeginTransaction();
//         }
//         return new NestedTransaction(this, Layer);
//     }

//     public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
//     {
//         if (Layer++ == 0) {
//            await  _sqlServerConnection.BeginTransactionAsync();
//         }
//         return new NestedTransaction(this, Layer);
//     }

//     public void CommitTransaction()
//     {
//         if (Layer-- <= 1) {
//             _sqlServerConnection.CommitTransaction();
//         }
//     }

//     public Task CommitTransactionAsync(CancellationToken cancellationToken = default)
//         => Layer-- <= 1 ? _sqlServerConnection.CurrentTransaction.CommitAsync(cancellationToken) : Task.CompletedTask;

//     public void ResetState()
//         => _sqlServerConnection.ResetState();

//     public Task ResetStateAsync(CancellationToken cancellationToken = default)
//         => _sqlServerConnection.ResetStateAsync(cancellationToken);

//     public void RollbackTransaction()
//         => _sqlServerConnection.RollbackTransaction();

//     public Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
//         => _sqlServerConnection.RollbackTransactionAsync();
// }

// internal class NestedTransaction  : IDbContextTransaction
// {
//     readonly NestedTransactionManager _manager;
//     readonly int _layer;

//     public NestedTransaction (NestedTransactionManager manager, int layer)
//     {
//         this._manager = manager;
//         this._layer = layer;
//     }

//     IDbContextTransaction Transaction => _manager.CurrentTransaction;

//     public Guid TransactionId => Transaction.TransactionId;

//     bool Commited => _layer > _manager.Layer;

//     public void Commit() => _manager.CommitTransaction();

//     public Task CommitAsync(CancellationToken cancellationToken = default)
//         => _manager.CommitTransactionAsync();

//     public void Dispose()
//     {
//         if (!Commited && Transaction != null) {
//             Transaction.Dispose();
//         }
//     }

//     public ValueTask DisposeAsync()
//         => !Commited && Transaction != null
//         ? Transaction.DisposeAsync()
//         : default;

//     public void Rollback() => Transaction.Rollback();


//     public Task RollbackAsync(CancellationToken cancellationToken = default)
//         => Transaction.RollbackAsync(cancellationToken);
// }
