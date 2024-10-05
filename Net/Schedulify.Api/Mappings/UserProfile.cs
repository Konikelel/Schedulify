using AutoMapper;
using Schedulify.App.Dtos;
using Schedulify.App.Entities;
using Schedulify.App.Models;
using Schedulify.Contracts.Requests;
using Schedulify.Contracts.Responses;

namespace Schedulify.Api.Mappings;

public class UserProfile: Profile
{
    public UserProfile()
    {
        CreateMap<UserBaseRequest, UserBaseDtos>()
            .ForMemberNewGuid(desc => desc.Id)
            .ForMemberFromItem(dest => dest.PasswordHash)
            .ForMemberFromItem(dest => dest.PasswordSalt)
            .ForMemberDateTimeOffsetNow(dest => dest.UpdatedAt);
        
        CreateMap<CreateScheduleRequest, CreateScheduleDto>()
            .IncludeBase<ScheduleBaseRequest, ScheduleBaseDto>()
            .ForMemberDateTimeOffsetNow(desc => desc.CreatedAt);

        CreateMap<UpdateScheduleRequest, UpdateScheduleDto>()
            .IncludeBase<ScheduleBaseRequest, ScheduleBaseDto>()
            .ForMember(
                dest => dest.Id, opt => 
                    opt.MapFrom(src => src.Id
            ));
        
        CreateMap<ScheduleEntity, ScheduleModel>()
            .ReverseMap(); // MODEL NOT EXTENDED
        
        //TODO: Add mapping for UserBaseResponses
    }
}