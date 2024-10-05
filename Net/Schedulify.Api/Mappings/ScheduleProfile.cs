using AutoMapper;
using Schedulify.App.Dtos;
using Schedulify.App.Entities;
using Schedulify.App.Models;
using Schedulify.Contracts.Requests;
using Schedulify.Contracts.Responses;

namespace Schedulify.Api.Mappings;

public class ScheduleProfile: Profile
{
    public ScheduleProfile()
    {
        CreateMap<CreateScheduleRequest, CreateScheduleDto>()
            .ForMemberNewGuid(desc => desc.Id)
            .ForMemberFromItem(dest => dest.OwnerId, "OwnerId");

        CreateMap<UpdateScheduleRequest, UpdateScheduleDto>()
            .ForMemberFromItem(dest => dest.OwnerId, "OwnerId");
        
        CreateMap<ScheduleEntity, ScheduleModel>()
            .ReverseMap(); // MODEL NOT EXTENDED
        
        CreateMap<ScheduleModel, CreateScheduleResponses>();
        CreateMap<ScheduleModel, UpdateScheduleRequest>();
    }
}