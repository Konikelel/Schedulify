using AutoMapper;
using Schedulify.App.Dtos;
using Schedulify.App.Entities;
using Schedulify.App.Models;
using Schedulify.Contracts.Requests;
using Schedulify.Contracts.Responses;

namespace Schedulify.API.Mappings;

public class ScheduleProfile: Profile
{
    public ScheduleProfile()
    {
        CreateMap<CreateScheduleRequest, CreateScheduleDto>()
            .ForMemberNewGuid(desc => desc.Id)
            .ForMemberFromItem(dest => dest.OwnerId, "OwnerId");

        CreateMap<UpdateScheduleRequest, UpdateScheduleDto>()
            .ForMemberNewGuid(desc => desc.Id);
        
        CreateMap<ScheduleEntity, ScheduleModel>()
            .ReverseMap(); // MODEL NOT EXTENDED
        
        CreateMap<ScheduleModel, CreateScheduleResponses>();
        CreateMap<ScheduleModel, UpdateScheduleRequest>();
    }
}