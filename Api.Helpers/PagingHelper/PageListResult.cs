using Microsoft.EntityFrameworkCore;

namespace Api.Helpers;

public class PageListResult<T> where T: class
{
    public Pagination Pagination { get; set; }
    public List<T> ListValue { get; set; }
    public PageListResult(List<T> items, Pagination pagination)
    {
        this.Pagination = new Pagination
        {
            TotalItems = pagination.TotalItems, 
            PageIndex = pagination.PageIndex,
            PageSize = pagination.PageSize,
            OrderBy = pagination.OrderBy,
            IsAscending = pagination.IsAscending,
            ThenBy = pagination.ThenBy,
            IsThenAscending = pagination.IsThenAscending
        };
        this.ListValue = [.. items];
    }

    public static async Task<PageListResult<T>> CreateAsync(IQueryable<T> sources, Pagination pagination)
    {
        if (pagination == null)
        {
            pagination = new Pagination();
        }
        if (sources==null) {
            return new PageListResult<T>(new List<T>(), pagination);                
        }

        pagination.TotalItems = await sources.CountAsync();

        if ((pagination.PageIndex * pagination.PageSize) > pagination.TotalItems)
        {
            pagination.PageIndex = (int)System.Math.Ceiling(pagination.TotalItems / (double)pagination.PageSize);
        }
        if (pagination.PageIndex < 1)
        {
            pagination.PageIndex = 1;
        }

        var items = await sources.Skip((pagination.PageIndex - 1) * pagination.PageSize).Take(pagination.PageSize).ToListAsync();

        return new PageListResult<T>(items, pagination);
    }

    public static async Task<PageListResult<T>> CreateAsync<S>(IQueryable<T> sources, Pagination pagination)
    {
        if (pagination == null)
        {
            pagination = new Pagination();
        }
        if (sources==null) {
            return new PageListResult<T>(new List<T>(), pagination);                
        }

        pagination.TotalItems = await sources.CountAsync();

        if ((pagination.PageIndex * pagination.PageSize) > pagination.TotalItems)
        {
            pagination.PageIndex = (int)System.Math.Ceiling(pagination.TotalItems / (double)pagination.PageSize);
        }
        if (pagination.PageIndex < 1)
        {
            pagination.PageIndex = 1;
        }

        var items = await sources.Skip((pagination.PageIndex - 1) * pagination.PageSize).Take(pagination.PageSize).ToListAsync();
        var dto = AgileObjects.AgileMapper.Mapper.Map(items).ToANew<List<T>>();
        return new PageListResult<T>(items, pagination);
    }


    public static async Task<PageListResult<T>> CreateAsync(IEnumerable<T> sources, Pagination pagination)
    {
        if (pagination == null)
        {
            pagination = new Pagination();
        }
        if (sources==null) {
            return new PageListResult<T>(new List<T>(), pagination);                
        }

        pagination.TotalItems = sources.Count();

        if (pagination.PageIndex < 1)
        {
            pagination.PageIndex = 1;
        }
        else if ((pagination.PageIndex * pagination.PageSize) > pagination.TotalItems)
        {
            pagination.PageIndex = (int)System.Math.Ceiling(pagination.TotalItems / (double)pagination.PageSize);
        }

        var items = sources.Skip((pagination.PageIndex - 1) * pagination.PageSize).Take(pagination.PageSize).ToList();
        await Task.CompletedTask;
        return new PageListResult<T>(items, pagination);
    }

    public static async Task<PageListResult<T>> CreateAsync(List<T> sources, Pagination pagination)
    {
        if (pagination == null)
        {
            pagination = new Pagination();
        }
        if (sources==null) {
            return new PageListResult<T>(new List<T>(), pagination);                
        }
        
        pagination.TotalItems = sources.Count();

        if (pagination.PageIndex < 1)
        {
            pagination.PageIndex = 1;
        }
        else if ((pagination.PageIndex * pagination.PageSize) > pagination.TotalItems)
        {
            pagination.PageIndex = (int)System.Math.Ceiling(pagination.TotalItems / (double)pagination.PageSize);
        }

        var items = sources.Skip((pagination.PageIndex - 1) * pagination.PageSize).Take(pagination.PageSize).ToList();
        await Task.CompletedTask;
        return new PageListResult<T>(items, pagination);
    }


}