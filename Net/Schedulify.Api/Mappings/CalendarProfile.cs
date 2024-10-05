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
            .ForMemberFromItem(dest => dest.OwnerId, "OwnerId")
            .ForMemberNewGuid(desc => desc.Id);

        CreateMap<UpdateCalendarRequest, UpdateCalendarDto>()
            .ForMemberFromItem(dest => dest.OwnerId, "OwnerId")
            .ForMemberNewGuid(desc => desc.UpdatedAt);
        
        CreateMap<CalendarModel, CategoryEntity>(); //MODEL EXTENDED
        
        CreateMap<CalendarModel, CreateCalendarResponse>();
        CreateMap<CalendarModel, UpdateCalendarResponse>();
    }
}