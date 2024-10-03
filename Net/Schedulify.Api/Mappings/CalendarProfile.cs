using AutoMapper;
using Schedulify.App.Dtos;
using Schedulify.App.Entities;
using Schedulify.App.Models;
using Schedulify.Contracts.Requests;
using Schedulify.Contracts.Responses;

namespace Schedulify.Apis.Mappings;

public class CalendarProfile: Profile
{
    public CalendarProfile()
    {
        CreateMap<CreateCalendarRequest, CreateCalendarDto>()
            .ForMemberNewGuid(desc => desc.Id)
            .ForMemberFromItem(dest => dest.OwnerId, "OwnerId");

        CreateMap<UpdateCalendarRequest, UpdateCalendarDto>()
            .ForMemberNewGuid(desc => desc.Id);
        
        CreateMap<CalendarModel, CategoryEntity>(); //MODEL EXTENDED
        
        CreateMap<CalendarModel, CreateCalendarResponse>();
        CreateMap<CalendarModel, UpdateCalendarResponse>();
    }
}