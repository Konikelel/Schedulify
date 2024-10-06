using System.Linq.Expressions;
using AutoMapper;

namespace Schedulify.Api;

public static class MapperExtensions
{
    public static TDestination MapUsingItems<TDestination>(this IMapper mapper, object source, object items)
    {
        return mapper.Map<TDestination>(source, opt =>
            {
                foreach (var item in items.GetType().GetProperties())
                {
                    opt.Items[item.Name] = item.GetValue(items);
                }
            });
    }

    public static IMappingExpression<TSource, TDestination> ForMemberFromItem<TSource, TDestination, TMember>(
        this IMappingExpression<TSource, TDestination> expr,
        Expression<Func<TDestination, TMember>> dest)
    {
        return expr.ForMember(dest, opt => opt.MapFrom((_, _, _, context) => context.Items[nameof(dest)]));
    }
    
    public static IMappingExpression<TSource, TDestination> ForMemberNewGuid<TSource, TDestination, TMember>(
        this IMappingExpression<TSource, TDestination> expr,
        Expression<Func<TDestination, TMember>> dest)
    {
        return expr.ForMember(dest, opt => opt.MapFrom(_ => Guid.NewGuid()));
    }
    
    public static IMappingExpression<TSource, TDestination> ForMemberDateTimeOffsetNow<TSource, TDestination, TMember>(
        this IMappingExpression<TSource, TDestination> expr,
        Expression<Func<TDestination, TMember>> dest)
    {
        return expr.ForMember(dest, opt => opt.MapFrom(_ => DateTimeOffset.Now));
    }
}