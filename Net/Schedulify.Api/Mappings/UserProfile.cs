using AutoMapper;
using Schedulify.App.Dtos;
using Schedulify.App.Entities;
using Schedulify.App.Models;
using Schedulify.Contracts.Requests;
using Schedulify.Contracts.Responses;

namespace Schedulify.Api.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        //TODO: Add mapping for UserRequests
        
        CreateMap<ScheduleEntity, ScheduleModel>()
            .ReverseMap();
        
        //TODO: Add mapping for UserResponses
    }
}