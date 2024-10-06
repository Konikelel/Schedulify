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
        CreateMap<CreateUserRequest, CreateUserDto>()
            .ForMemberNewGuid(dest => dest.Id)
            .ForMemberFromItem(dest => dest.PasswordHash)
            .ForMemberFromItem(dest => dest.PasswordSalt)
            .ForMemberDateTimeOffsetNow(dest => dest.CreatedAt)
            .ForMemberDateTimeOffsetNow(dest => dest.UpdatedAt);

        CreateMap<UpdateUserRequest, UpdateUserDto>()
            .ForMemberFromItem(dest => dest.PasswordHash)
            .ForMemberFromItem(dest => dest.PasswordSalt)
            .ForMemberDateTimeOffsetNow(dest => dest.UpdatedAt);
        
        CreateMap<ScheduleEntity, ScheduleModel>()
            .ReverseMap(); // MODEL NOT EXTENDED
        
        //TODO: Add mapping for UserBaseResponses
    }
}