using AutoMapper;
using Schedulify.App.Dtos;
using Schedulify.App.Entities;
using Schedulify.App.Models;
using Schedulify.Contracts.Requests;
using Schedulify.Contracts.Responses;

namespace Schedulify.Api.Mappings;

public class CalendarProfile: Profile
{
    public CalendarProfile()
    {
        CreateMap<CreateCalendarRequest, CreateCalendarDto>()
            .ForMemberNewGuid(dest => dest.Id)
            .ForMemberFromItem(dest => dest.OwnerId)
            .ForMemberDateTimeOffsetNow(dest => dest.CreatedAt)
            .ForMemberDateTimeOffsetNow(dest => dest.UpdatedAt);
        CreateMap<UpdateCalendarRequest, UpdateCalendarDto>()
            .ForMemberFromItem(dest => dest.Id)
            .ForMemberFromItem(dest => dest.OwnerId)
            .ForMemberDateTimeOffsetNow(dest => dest.UpdatedAt);

        CreateMap<CalendarModel, CategoryEntity>()
            .ReverseMap();
        CreateMap<IEnumerable<CategoryModel>, GetMultipleCategoryResponse>()
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src));
        
        CreateMap<CalendarModel, CreateCalendarResponse>();
        CreateMap<CalendarModel, UpdateCalendarResponse>();
    }
}