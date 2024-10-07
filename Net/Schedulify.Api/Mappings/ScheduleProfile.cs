using AutoMapper;
using Schedulify.App.Dtos;
using Schedulify.App.Entities;
using Schedulify.App.Models;
using Schedulify.Contracts.Requests;
using Schedulify.Contracts.Responses;

namespace Schedulify.Api.Mappings;

public class ScheduleProfile : Profile
{
    public ScheduleProfile()
    {
        CreateMap<CreateScheduleRequest, CreateScheduleDto>()
            .ForMemberNewGuid(dest => dest.Id)
            .ForMemberFromItem(dest => dest.OwnerId)
            .ForMemberDateTimeOffsetNow(dest => dest.CreatedAt)
            .ForMemberDateTimeOffsetNow(dest => dest.UpdatedAt);
        
        CreateMap<UpdateScheduleRequest, UpdateScheduleDto>()
            .ForMemberFromItem(dest => dest.Id)
            .ForMemberFromItem(dest => dest.OwnerId)
            .ForMemberDateTimeOffsetNow(dest => dest.UpdatedAt);
        
        CreateMap<ScheduleEntity, ScheduleModel>()
            .ReverseMap();
        
        CreateMap<ScheduleModel, CreateScheduleResponses>();
        CreateMap<ScheduleModel, UpdateScheduleRequest>();
    }
}