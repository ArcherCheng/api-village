using AgileObjects.AgileMapper;
using System.Linq;

namespace Api.Helpers;

public static class MapperExtensions
{
    public static TSource Clone<TSource>(this TSource source)
    {
        return AgileObjects.AgileMapper.Mapper.DeepClone(source);
    }

    public static TDestination Map<TSource, TDestination>(this TSource source)
    {
        return AgileObjects.AgileMapper.Mapper.Map(source).ToANew<TDestination>(); 
    }

    public static TDestination Map<TDestination>(this object source)
    {
        return AgileObjects.AgileMapper.Mapper.Map(source).ToANew<TDestination>();
    }

    public static TDestination Map<TSource, TDestination>(this TSource source, TDestination destination)
    {
        return AgileObjects.AgileMapper.Mapper.Map(source).OnTo(destination);
    }

    public static IQueryable<TDestination> Project<TSource, TDestination>(this IQueryable<TSource> queryable)
    {
        return queryable.Project().To<TDestination>();
    }
}