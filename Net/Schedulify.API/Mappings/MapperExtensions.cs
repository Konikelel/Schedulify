using System.Linq.Expressions;
using AutoMapper;

namespace Schedulify.API.Mappings;

public static class MapperExtensions
{
    public static TDestination MapUsingItems<TSource, TDestination>(this IMapper mapper, TSource source, Dictionary<string, string> items)
    {
        return mapper.Map<TSource, TDestination>(source, opt =>
            {
                foreach (var item in items)
                {
                    opt.Items[item.Key] = item.Value;
                }
            });
    }

    public static IMappingExpression<TSource, TDestination> ForMemberFromItem<TSource, TDestination, TMember>(
        this IMappingExpression<TSource, TDestination> expr,
        Expression<Func<TDestination, TMember>> dest,
        string itemKey)
    {
        return expr.ForMember(dest, opt => opt.MapFrom((_, _, _, context) => context.Items[itemKey]));
    }
    
    public static IMappingExpression<TSource, TDestination> ForMemberNewGuid<TSource, TDestination, TMember>(
        this IMappingExpression<TSource, TDestination> expr,
        Expression<Func<TDestination, TMember>> dest)
    {
        return expr.ForMember(dest, opt => opt.MapFrom(_ => Guid.NewGuid()));
    }
}