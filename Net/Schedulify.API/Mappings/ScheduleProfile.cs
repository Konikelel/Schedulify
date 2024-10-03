using AutoMapper;
using Schedulify.App.Entities;
using Schedulify.App.Models;
using Schedulify.Contracts.Requests;
using Schedulify.Contracts.Responses;

namespace Schedulify.API.Mappings;

public class ScheduleProfile: Profile
{
    public ScheduleProfile()
    {
        CreateMap<ScheduleEntity, ScheduleModel>(); // NOT EXTENDED
        
        CreateMap<ScheduleModel, CreateScheduleResponses>();
        CreateMap<ScheduleModel, UpdateScheduleRequest>();
    }
}