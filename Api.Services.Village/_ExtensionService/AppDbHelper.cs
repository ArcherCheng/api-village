using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Api.Models;
using Api.Helpers;
using Microsoft.Reporting.NETCore;
using System.Linq.Expressions;

namespace Api.Services;
public static class AppDbHelper
{
    //static readonly ILogger _logger;

    // static AppDbHelper()
    // {
    //     //_logger = Api.Helpers.MyFileLoggerFactory.CreateLogger("AppHrDbHelper");
    //     //_db = new AppDbContext();
    // }

    static AppDbContext NewDb()
    {
        return new AppDbContext();
    }

    #region GetById
    public static TEntity? GetById<TEntity>(string id, AppDbContext? db = null) where TEntity : class
    {
        db ??= NewDb();
        var result = db.Set<TEntity>().Find(id);
        return result;
    }

    public static async Task<TEntity?> GetByIdAsync<TEntity>(string id, AppDbContext? db = null) where TEntity : class
    {
        db ??= NewDb();
        var result = await db.Set<TEntity>().FindAsync(id);
        return result;
    }

    public static TEntity? GetById<TEntity>(int id, AppDbContext? db = null) where TEntity : class
    {
        db ??= NewDb();
        var result = db.Set<TEntity>().Find(id);
        return result;
    }

    public static async Task<TEntity?> GetByIdAsync<TEntity>(int id, AppDbContext? db = null) where TEntity : class
    {
        db ??= NewDb();
        var result = await db.Set<TEntity>().FindAsync(id);
        return result;
    }

    public static TEntity? GetById<TEntity>(Guid id, AppDbContext? db = null) where TEntity : class
    {
        db ??= NewDb();
        var result = db.Set<TEntity>().Find(id);
        return result;
    }

    public static async Task<TEntity?> GetByIdAsync<TEntity>(Guid id, AppDbContext? db = null) where TEntity : class
    {
        db ??= NewDb();
        var result = await db.Set<TEntity>().FindAsync(id);
        return result;
    }

    public static TEntity? GetById<TEntity>(DateTime id, AppDbContext? db = null) where TEntity : class
    {
        db ??= NewDb();
        var result = db.Set<TEntity>().Find(id);
        return result;
    }

    public static async Task<TEntity?> GetByIdAsync<TEntity>(DateTime id, AppDbContext? db = null) where TEntity : class
    {
        db ??= NewDb();
        var result = await db.Set<TEntity>().FindAsync(id);
        return result;
    }

    public static TEntity? GetById<TEntity>(DateOnly id, AppDbContext? db = null) where TEntity : class
    {
        db ??= NewDb();
        var result = db.Set<TEntity>().Find(id);
        return result;
    }

    public static async Task<TEntity?> GetByIdAsync<TEntity>(DateOnly id, AppDbContext? db = null) where TEntity : class
    {
        db ??= NewDb();
        var result = await db.Set<TEntity>().FindAsync(id);
        return result;
    }
    #endregion

    #region GetFirstOrDefault
    public static TEntity? GetFirstOrDefault<TEntity>(Expression<Func<TEntity, bool>> expression, AppDbContext? db = null) where TEntity : class
    {
        db ??= NewDb();
        var result = db.Set<TEntity>().AsNoTracking().FirstOrDefault<TEntity>(expression);
        return result;
    }

    public static async Task<TEntity?> GetFirstOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> expression, AppDbContext? db = null) where TEntity : class
    {
        db ??= NewDb();
        var result = await db.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync<TEntity>(expression);
        return result;
    }
    #endregion

    #region GetAll
    public static bool GetAny<TEntity>(Expression<Func<TEntity, bool>> expression, AppDbContext? db = null) where TEntity : class
    {
        db ??= NewDb();
        var result = db.Set<TEntity>().Any<TEntity>(expression);
        return result;
    }
    public static List<TEntity> GetAll<TEntity>(Expression<Func<TEntity, bool>>? expression = null, AppDbContext? db = null) where TEntity : class
    {
        db ??= NewDb();
        var result = db.Set<TEntity>().AsNoTracking();
        if (expression != null)
            result = result.Where(expression);
        return result.ToList();
    }

    public static async Task<List<TEntity>> GetAllAsync<TEntity>(Expression<Func<TEntity, bool>>? expression=null,AppDbContext? db = null) where TEntity : class
    {
        db ??= NewDb();
        var result = db.Set<TEntity>().AsNoTracking();
        if (expression != null)
            result = result.Where(expression);
        return await result.ToListAsync();
    }
    #endregion

    #region generic Get
    public static IList<TEntity> GetAllOrder<TEntity>(Expression<Func<TEntity, bool>>? filter = null
        , Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null
        , AppDbContext? db = null
        // , params Expression<Func<TEntity, object>>[] includes
        ) where TEntity : class
    {
        db ??= NewDb();
        DbSet<TEntity> dbSet;
        dbSet = db.Set<TEntity>();

        IQueryable<TEntity> query = dbSet;

        // foreach (Expression<Func<TEntity, object>> include in includes)
        //     query = query.Include(include);

        //if (select != null)
        //    query = query.Select(select);
        if (filter != null)
            query = query.Where(filter);

        if (orderBy != null)
            query = orderBy(query);

        return query.ToList();
    }
    #endregion

    #region crud
    public static async Task<TEntity> AddAsync<TEntity>(TEntity entity,AppDbContext? db = null) where TEntity : class
    {
        db ??= NewDb();
        db.Set<TEntity>().Add(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    public static async Task<TEntity> UpdateAsync<TEntity>(TEntity entity,Expression<Func<TEntity, bool>>? expression = null,AppDbContext? db = null) where TEntity : class
    {
        db ??= NewDb();
        db.Set<TEntity>().Update(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    public static async Task DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> expression,AppDbContext? db = null) where TEntity : class
    {
        db ??= NewDb();
        var entity = await db.Set<TEntity>().FirstOrDefaultAsync<TEntity>(expression);
        if (entity != null) {
            db.Set<TEntity>().Remove(entity);
            await db.SaveChangesAsync();
        }
        return ;
    }
    #endregion

    public static async Task AddAppLogMessage(SendMessageResult sendResult)
    {
        await Task.CompletedTask;
        // var db = NewDb();
        // db.AppLogMessage.Add(new AppLogMessage
        // {
        //     Id = 0,
        //     IsSuccess = sendResult.IsSuccess,
        //     SendType = sendResult.SendType,
        //     SendNo = sendResult.SendNo??"undefined",
        //     SendMessage = sendResult.SendMessage,
        //     SendSubject = sendResult.SendSubject,
        //     ErrorMessage = sendResult.ErrorMessage.ToSubstring(0,3900),
        //     SendDate = DateTime.Now
        // });
        // await db.SaveChangesAsync();
    }
}

