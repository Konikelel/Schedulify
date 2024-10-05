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
        CreateMap<CalendarBaseRequest, CalendarBaseDto>()
            .ForMemberNewGuid(dest => dest.Id)
            .ForMemberFromItem(dest => dest.OwnerId)
            .ForMemberDateTimeOffsetNow(dest => dest.UpdatedAt);
        
        CreateMap<CreateCalendarRequest, CreateCalendarDto>()
            .IncludeBase<CalendarBaseRequest, CalendarBaseDto>()
            .ForMemberDateTimeOffsetNow(dest => dest.CreatedAt);

        CreateMap<UpdateCalendarRequest, UpdateCalendarDto>()
            .IncludeBase<CalendarBaseRequest, CalendarBaseDto>()
            .ForMember(
                dest => dest.Id, opt => 
                    opt.MapFrom(src => src.Id
            ));
        
        CreateMap<CalendarModel, CategoryEntity>(); //MODEL EXTENDED
        
        CreateMap<CalendarModel, CreateCalendarResponse>();
        CreateMap<CalendarModel, UpdateCalendarResponse>();
    }
}