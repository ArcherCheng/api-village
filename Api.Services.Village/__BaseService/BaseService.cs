using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using Microsoft.Reporting.NETCore;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Api.Helpers;
using Api.Models;

namespace Api.Services;

public class BaseService<T> : IBaseService  where T : IBaseService
{
    protected ILogger<T> _logger;
    protected Microsoft.AspNetCore.Http.IHttpContextAccessor _httpContextAccessor;
    protected ApiUserData _apiUserData;

    public BaseService()
    {
        this._logger = Api.Helpers.MyFileLoggerFactory.CreateLogger<T>();
        this._httpContextAccessor = Api.Services.ServiceLocator.Current.GetInstance<Microsoft.AspNetCore.Http.IHttpContextAccessor>();
        this._apiUserData = this.GetApiUserData();
    }

    public BaseService(ILogger<T> logger,Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor)
    {
        this._logger = logger;
        this._httpContextAccessor = httpContextAccessor;
        this._apiUserData = this.GetApiUserData();
    }

    public AppDbContext NewDb()
    {
        return new AppDbContext();
        //return new Api.Models.Village.AppDbContext();
    }

    #region GetById
    public virtual TEntity? GetById<TEntity>(object id)
         where TEntity : BaseEntity, IBaseEntity
    {
        using var db = NewDb();
        var instance = Activator.CreateInstance<TEntity>();
        var type = instance.GetKeyType();
        switch (type.ToLower())
        {
            case "string":
                return db.Set<TEntity>().Find(id.ToString());
            case "int":
                int intId = 0;
                _ = int.TryParse(id.ToString(), out intId);
                return db.Set<TEntity>().Find(intId);
            case "long":
                long longId = 0;
                _ = long.TryParse(id.ToString(), out longId);
                return db.Set<TEntity>().Find(longId);
            case "uuid":
            case "guid":
                Guid guidId = new Guid();
                _ = Guid.TryParse(id.ToString(), out guidId);
                return db.Set<TEntity>().Find(guidId);
            case "decimal":
                decimal decId = 0m;
                _ = decimal.TryParse(id.ToString(), out decId);
                return db.Set<TEntity>().Find(decId);
            case "date":
            case "datetime":
                DateTime dateId = DateTime.Today;
                _ = DateTime.TryParse(id.ToString(), out dateId);
                return db.Set<TEntity>().Find(dateId);
            default:
                return db.Set<TEntity>().Find(id.ToString());
        }
    }


    public virtual async Task<TEntity?> GetByIdAsync<TEntity>(object id)
        where TEntity : BaseEntity, IBaseEntity
    {
        using var db = NewDb();
        TEntity instance = Activator.CreateInstance<TEntity>();
        string type = instance.GetKeyType();
        switch (type.ToLower())
        {
            case "string":
                return await db.Set<TEntity>().FindAsync(id.ToString());
            case "int":
                int intId = 0;
                _ = int.TryParse(id.ToString(), out intId);
                return await db.Set<TEntity>().FindAsync(intId);
            case "uuid":
            case "guid":
                Guid guidId = new Guid();
                _ = Guid.TryParse(id.ToString(), out guidId);
                return await db.Set<TEntity>().FindAsync(guidId);
            case "decimal":
                decimal decId = 0m;
                _ = decimal.TryParse(id.ToString(), out decId);
                return await db.Set<TEntity>().FindAsync(decId);
            case "long":
                long longId = 0;
                _ = long.TryParse(id.ToString(), out longId);
                return await db.Set<TEntity>().FindAsync(longId);
            case "date":
            case "datetime":
                DateTime dateId = DateTime.Today;
                _ = DateTime.TryParse(id.ToString(), out dateId);
                return await db.Set<TEntity>().FindAsync(dateId);
            default:
                return await db.Set<TEntity>().FindAsync(id.ToString());
        }
    }

     #endregion

    #region GetFirstOrDefault
    public virtual TEntity? GetFirstOrDefault<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class
    {
        using var db = NewDb();
        var result = db.Set<TEntity>().AsNoTracking().FirstOrDefault<TEntity>(expression);
        return result;
    }

    public virtual async Task<TEntity?> GetFirstOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class
    {
        using var db = NewDb();
        var result = await db.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync<TEntity>(expression);
        return result;
    }
    #endregion

    #region GetAll
    public virtual IList<TEntity> GetAll<TEntity>() where TEntity : class
    {
        using var db = NewDb();
        var result = db.Set<TEntity>().AsNoTracking();
        //result = result.FilterDepIdByUserData(_userData.UserData);
        return result.ToList();
    }

    public virtual IList<TEntity> GetAll<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class
    {
        using var db = NewDb();
        var result = db.Set<TEntity>().AsNoTracking().Where<TEntity>(expression);
        //result = result.FilterDepIdByUserData(_userData.UserData);
        return result.ToList();
    }

    public virtual async Task<IList<TEntity>> GetAllAsync<TEntity>() where TEntity : class
    {
        using var db = NewDb();
        var result = db.Set<TEntity>().AsNoTracking().AsQueryable();
        //result = result.FilterDepIdByUserData(_userData.UserData);
        return await result.ToListAsync();
    }

    public virtual async Task<IList<TEntity>> GetAllAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class
    {
        using var db = NewDb();
        var result = db.Set<TEntity>().AsNoTracking().Where<TEntity>(expression);
        //result = result.FilterDepIdByUserData(_userData.UserData);
        return await result.ToListAsync();
    }
    #endregion

    #region GetViewList by BaseParas
    public virtual async Task<IList<TView>> GetViewListAsync<TView>(BaseParas baseParas) where TView : class
    {
        using var db = NewDb();
        var filterPredicate = Api.Helpers.WhereExtensions.BuildWhereExpression<TView>(baseParas.WhereConditionList);
        var result = db.Set<TView>().AsNoTracking().Where(filterPredicate).AsQueryable();
        //result = result.FilterDepIdByUserData(_userData.UserData);
        if (baseParas.Pagination != null && !string.IsNullOrWhiteSpace(baseParas.Pagination.OrderBy)){
            result = result.OrderByCustom(baseParas.Pagination.OrderBy, baseParas.Pagination.IsAscending);
        }
        if (baseParas.Pagination != null && !string.IsNullOrWhiteSpace(baseParas.Pagination.ThenBy)){
            result = result.ThenByCustom(baseParas.Pagination.ThenBy, baseParas.Pagination.IsThenAscending);
        }
        if (baseParas.Pagination != null && !string.IsNullOrWhiteSpace(baseParas.Pagination.ThreeBy)){
            result = result.ThenByCustom(baseParas.Pagination.ThreeBy, baseParas.Pagination.IsThreeAscending);
        }
        return await result.ToListAsync();
    }

    public virtual async Task<PageListResult<TView>> GetViewPageListAsync<TView>(BaseParas baseParas) where TView : class
    {
        using var db = NewDb();
        var filterPredicate = Api.Helpers.WhereExtensions.BuildWhereExpression<TView>(baseParas.WhereConditionList);
        var result = db.Set<TView>().AsNoTracking().Where(filterPredicate).AsQueryable();
        //result = result.FilterDepIdByUserData(_userData.UserData);
        if (baseParas.Pagination != null && !string.IsNullOrWhiteSpace(baseParas.Pagination.OrderBy)){
            result = result.OrderByCustom(baseParas.Pagination.OrderBy, baseParas.Pagination.IsAscending);
        }
        if (baseParas.Pagination != null && !string.IsNullOrWhiteSpace(baseParas.Pagination.ThenBy)){
            result = result.ThenByCustom(baseParas.Pagination.ThenBy, baseParas.Pagination.IsThenAscending);
        }
        if (baseParas.Pagination != null && !string.IsNullOrWhiteSpace(baseParas.Pagination.ThreeBy)){
            result = result.ThenByCustom(baseParas.Pagination.ThreeBy, baseParas.Pagination.IsThreeAscending);
        }
        return await PageListResult<TView>.CreateAsync(result, baseParas.Pagination!);
    }

    public virtual async Task<TView?> GetViewFirstAsync<TView>(BaseParas baseParas) where TView : class
    {
        using var db = NewDb();
        var filterPredicate = Api.Helpers.WhereExtensions.BuildWhereExpression<TView>(baseParas.WhereConditionList);
        var result = db.Set<TView>().AsNoTracking().FirstOrDefaultAsync(filterPredicate);
        return await result;
    }
    #endregion

    #region Async CRUD add update delete with saveChanges()
    public virtual async Task AddAsync<TEntity>(TEntity entity) where TEntity : BaseEntity, IBaseEntity
    {
        using var db = NewDb();
        var transaction = await db.Database.BeginTransactionAsync();
        try
        {
            entity.WriteInfo = GetWriteInfo();
            db.Set<TEntity>().Add(entity);
            await db.SaveChangesAsync();
            await UpdateRelationTablesAsync(db,entity,null);
            await transaction.CommitAsync();
        }
        catch (System.Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex.ToString());
            throw;
        }
        finally
        {
            await transaction.DisposeAsync();
        }
    }

    public virtual async Task UpdateAsync<TEntity>(object id,TEntity entity) where TEntity : BaseEntity, IBaseEntity
    {
        using var db = NewDb();
        var transaction = await db.Database.BeginTransactionAsync();
        try
        {
            var updateEntity = await this.GetByIdAsync<TEntity>(id) ?? throw new Exception($"update id: {id} not found in table");
            //先處理oldEntity明細檔之被刪除的資料,可用linq.Except()
            var oldCopyEntity = AgileObjects.AgileMapper.Mapper.Map(updateEntity).ToANew<TEntity>();
            //await RemoveMasterDetailsExceptItemsAsync(db,id,entity,updateEntity);
            // entity.CreateUser = updateEntity.CreateUser;
            // entity.BatchUser  = updateEntity.BatchUser;

            //要加入 detached 才不會更新到 relation table
            //db.Entry(updateEntity).CurrentValues.SetValues(entity);
            db.Entry(updateEntity).State = EntityState.Detached;
            AgileObjects.AgileMapper.Mapper.Map(entity).Over(updateEntity);
            updateEntity.WriteInfo = GetWriteInfo();
            db.Set<TEntity>().Update(updateEntity);
            await db.SaveChangesAsync();
            await UpdateRelationTablesAsync(db,updateEntity,oldCopyEntity);
            await transaction.CommitAsync();
        }
        catch (System.Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex.ToString());
            throw;
        }
        finally
        {
            await transaction.DisposeAsync();
        }
    }


    public virtual async Task DeleteAsync<TEntity>(object id) where TEntity : BaseEntity, IBaseEntity
    {
        using var db = NewDb();
        var transaction = await db.Database.BeginTransactionAsync();
        try
        {
            var deleteEntity = await this.GetByIdAsync<TEntity>(id) ?? throw new Exception($"delete id: {id} not found in table");
            var oldCopyEntity = AgileObjects.AgileMapper.Mapper.Map(deleteEntity).ToANew<TEntity>();
            //先處理oldEntity明細檔之被刪除的資料,可用linq.Except()
            deleteEntity.WriteInfo = GetWriteInfo();
            db.Set<TEntity>().Update(deleteEntity);
            await db.SaveChangesAsync();

            db.Set<TEntity>().Remove(deleteEntity);  //直接刪除實體記錄,可考慮是否用邏輯註記刪除。
            await db.SaveChangesAsync();
            await UpdateRelationTablesAsync(db,null,oldCopyEntity);
            await transaction.CommitAsync();
        }
        catch (System.Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex.ToString());
            throw ;
        }
        finally
        {
            await transaction.DisposeAsync();
        }
    }

    public virtual async Task UpdateRelationTablesAsync<TEntity>(AppDbContext db, TEntity? newEntity, TEntity? oldEntity) where TEntity : BaseEntity, IBaseEntity
    {
        await Task.CompletedTask;
        return;
    }

    // always implement at child instance-service
    public virtual async Task<string?> ValidationAsync<TEntity>(TEntity model, CrudMode crudMode=CrudMode.Insert)
        where TEntity : BaseEntity, IBaseEntity
    {
        // var model = entity as Models.Hm2adj10;
        // if (!this.CheckInputDate(model.AtDate))
        //     return("異動日期超出系統日期設定範圍");

        // var hm1Emp10 = await AppHrHelper.GetHm1Emp10Async(model.EmpId);
        // if (model.AtDate < hm1Emp10.InDate)
        //     return("異動日期不能小於到職日期");

        // if (hm1Emp10.OutDate!= null && model.AtDate > hm1Emp10.OutDate)
        //     return("異動日期不能大於離職日期");
        await Task.CompletedTask;
        return null;
    }
    #endregion

    #region Master-details CRUD sample code
    /*
    public async Task AddMasterDetailAsync(Ok2Bbs10 entity)
    {
        using var db = NewDb();
        var transaction = await db.Database.BeginTransactionAsync();
        try
        {
            entity.WriteCreateUser(_userData);
            db.Ok2Bbs10s.Add(entity);
            await db.SaveChangesAsync();
            var result = await db.Ok2Bbs10s.Include(x => x.Ok2Bbs11s).FirstOrDefaultAsync(x =>x.BbsId == entity.BbsId);
            await UpdateRelationTablesAsync(db,result,null);
            await transaction.CommitAsync();
        }
        catch (System.Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
        finally
        {
            await transaction.DisposeAsync();
        }
    }

    public async Task UpdateMasterDetailAsync(int id,Ok2Bbs10 entity)
    {
        using var db = NewDb();
        var transaction = await db.Database.BeginTransactionAsync();
        try
        {
            ////step 1: get old data to clear relative table data
            var oldData = await db.Ok2Bbs10s.AsNoTracking().Include(x => x.Ok2Bbs11s).FirstOrDefaultAsync(x =>x.BbsId == id);
            //// 修改 master-details 修改資料時，必須先刪除 details 被刪除的資料列。
            await RemoveMasterDetailsExceptItemsAsync(db, id,entity,oldData);

            ////step 2: insert new data to db and relative table data
            var newData = AgileObjects.AgileMapper.Mapper.Map(entity).Over(oldData);
            newData.WriteUpdateUser(_userData);
            db.Ok2Bbs10s.Update(newData);
            await db.SaveChangesAsync();
            await UpdateRelationTablesAsync(db,entity,oldData);
            await transaction.CommitAsync();
        }
        catch (System.Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
        finally
        {
            await transaction.DisposeAsync();
        }
    }

    public async Task DeleteMasterDetailAsync(int id)
    {
        using var db = NewDb();
        var transaction = await db.Database.BeginTransactionAsync();
        try
        {
            var oldData = await db.Ok2Bbs10s.Include(x => x.Ok2Bbs11s).FirstOrDefaultAsync(x =>x.BbsId == id);
            db.Ok2Bbs10s.Remove(oldData);
            await db.SaveChangesAsync();
            await UpdateRelationTablesAsync(db,null,oldData);
            await transaction.CommitAsync();
        }
        catch (System.Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
        finally
        {
            await transaction.DisposeAsync();
        }
    }

    //// 修改 master-details 修改資料時，必須先刪除 details 被刪除的資料列。
    private async Task RemoveMasterDetailsExceptItemsAsync(AppDbContext db, object id, Ok2Bbs10 newEntity, Ok2Bbs10 oldEntity)
    {
        var exceptIds = (oldEntity.Ok2Bbs11s.Select(x => x.Id)).Except(newEntity.Ok2Bbs11s.Select(x => x.Id));
        foreach (var keyId in exceptIds)
        {
            var data = await db.Ok2Bbs11s.FindAsync(keyId);
            db.Ok2Bbs11s.Remove(data);
        }
    }
    */
    #endregion

    #region Report
    public List<ReportParameter> GetReportParameters(ReportSignParas reportSignParas )
    {
        string imageBase64Str = Api.Helpers.ImageHelper.GetLogoImageBase64String();
        // if (reportSignParas.ReportCompany.IsNullOrEmpty()) {
        //     reportSignParas.ReportCompany=_userData.CompanyName;
        // }
        var reportParameters = new List<ReportParameter>
        {
            // new("ReportCompany", reportSignParas.ReportCompany),
            new("ReportTitle", reportSignParas.ReportTitle),
            new("ReportNotes", reportSignParas.ReportNotes),
            new("ReportUser", _apiUserData?.UserName??""),
            new("ReportSign1", reportSignParas.ReportSign1),
            new("ReportSign2", reportSignParas.ReportSign2),
            new("ReportSign3", reportSignParas.ReportSign3),
            new("ReportSign4", reportSignParas.ReportSign4),
            new("ReportSign5", reportSignParas.ReportSign5),
            new("LogoImage", imageBase64Str),
            new("IsExecutionTime", reportSignParas.IsExecutionTime.ToString())
        };
        return reportParameters;
    }

    #endregion

    // #region Batch
    // public virtual async Task<IList<AppBatchMsg>> GetAppBatchMsgAsync(string batchName)
    // {
    //     var result = await db.AppBatchMsgs.AsNoTracking().Where(x => x.BatchName == batchName).ToListAsync();
    //     return result;
    // }
    // #endregion

    #region Dapper
    // public async Task<TEntity> FindByIdAsync<TEntity>(string id)
    // {
    //     // using (var cn = new Microsoft.Data.SqlClient.SqlConnection(Api.Helpers.AppSettingsHelper.ConnectionStr()))
    //     // {
    //     //     updateRows += await cn.ExecuteAsync(updateSql, new {month=batchParas.BatchMonth, ids = empIdList });
    //     // }
    //     // using var db = NewDb();
    //     // using var transaction = db.Database.GetDbConnection().BeginTransaction();
    //     // // var policy = new Microsoft.Extensions.ObjectPool.DefaultPooledObjectPolicy<Hm1Emp10>();
    //     // // var pool = new Microsoft.Extensions.ObjectPool.DefaultObjectPool<Hm1Emp10>(policy);
    //     // string tableName = typeof(TEntity).Name;
    //     // // Type type = Type.GetType("Api.Models."+tableName);
    //     // var instance = Activator.CreateInstance<TEntity>();
    //     // PropertyInfo key = instance.GetType().GetProperties()
    //     //     .FirstOrDefault(x => x.GetCustomAttributes().Any(a => ((System.ComponentModel.DataAnnotations.KeyAttribute)a) != null));
    //     // string sql = $"Select * from {tableName} Where {key.Name} = @id";
    //     // return await db.Database.GetDbConnection().QuerySingleAsync<TEntity>(sql);
    // }

    // public virtual void Dispose()
    // {
    //     db.Dispose();
    // }

    #endregion

    #region child override sample

    #endregion
    public ApiUserData GetApiUserData()
    {
        var claimsPrincipal = this._httpContextAccessor.HttpContext?.User;
        if (claimsPrincipal == null || claimsPrincipal.Identity == null || !claimsPrincipal.Identity.IsAuthenticated || !claimsPrincipal.Claims.Any()) {
            return new ApiUserData();
        }
        return claimsPrincipal.GetCurrentApiUserData();
    }

    public string GetWriteInfo()
    {
        var ip = GetClientIp();
        var userNameAndIpAddr = $"name={_apiUserData.UserName},userId={_apiUserData.UserId},teamId={_apiUserData.TeamId},time={DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")},ip={ip}";
        return userNameAndIpAddr;
    }

    public string GetClientIp()
    {
        var ip = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
        if (_httpContextAccessor.HttpContext?.Request.Headers.TryGetValue("X-Forwarded-For", out Microsoft.Extensions.Primitives.StringValues value) == true)
        {
            ip = value.FirstOrDefault()!;
        }
        ip ??= "HttpContext ip is null";
        return ip;
    }

}