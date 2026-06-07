using System.Linq.Expressions;
using Api.Helpers;
using Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Reporting.NETCore;

namespace Api.Services;

public interface IBaseService //: IDisposable
{
    //查詢資料用,不放入權限控管
    //string GetKeyType<TEntity>() where TEntity : BaseEntity, IBaseEntity;
    TEntity? GetById<TEntity>(object id) where TEntity : BaseEntity, IBaseEntity;
    Task<TEntity?> GetByIdAsync<TEntity>(object id) where TEntity : BaseEntity, IBaseEntity;
    TEntity? GetFirstOrDefault<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class;
    Task<TEntity?> GetFirstOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class;

    IList<TEntity> GetAll<TEntity>() where TEntity : class;
    IList<TEntity> GetAll<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class;
    Task<IList<TEntity>> GetAllAsync<TEntity>() where TEntity : class;
    Task<IList<TEntity>> GetAllAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class;
    // for report
    Task<TView?> GetViewFirstAsync<TView>(BaseParas baseParas) where TView : class;
    Task<IList<TView>> GetViewListAsync<TView>(BaseParas baseParas) where TView : class;

    //以下要做權限控制
    //顯示資料用,要排序,要分頁等
    Task<PageListResult<TView>> GetViewPageListAsync<TView>(BaseParas baseParas) where TView : class;

    //新增資料
    Task AddAsync<TEntity>(TEntity model) where TEntity : BaseEntity, IBaseEntity;
    // Task AddByDtoAsync<TDto, TEntity>(TDto modelDto)
    //     where TDto : BaseDto
    //     where TEntity : BaseEntity, IBaseEntity;

    //修改資料
    Task UpdateAsync<TEntity>(object id,TEntity model ) where TEntity : BaseEntity, IBaseEntity;

    //刪除資料
    Task DeleteAsync<TEntity>(object id) where TEntity : BaseEntity, IBaseEntity;

    //Task UpdateRelationTablesAsync<TEntity>(AppDbContext db, TEntity newEntity, TEntity oldEntity) where TEntity : BaseEntity, IBaseEntity;

    //// 修改 master-details 修改資料時，必須先刪除 details 被刪除的資料列。
    // Task DeleteMasterDetailsExceptItemsAsync<TEntity>(AppDbContext db, object id, TEntity newEntity, TEntity oldEntity) where TEntity : BaseEntity, IBaseEntity;

    //資料驗證
    Task<string?> ValidationAsync<TEntity>(TEntity model, CrudMode crudMode = CrudMode.Insert) where TEntity : BaseEntity, IBaseEntity;

    //Report
    List<ReportParameter> GetReportParameters(ReportSignParas reportSignParas);

    // //Batch Process
    // Task<IList<AppBatchMsg>> GetAppBatchMsgAsync(string batchName);
    ApiUserData GetApiUserData();
    string GetWriteInfo();
    string GetClientIp();
}
