using AutoMapper;
using Schedulify.App.Dtos;
using Schedulify.App.Entities;
using Schedulify.App.Models;
using Schedulify.Contracts.Requests;
using Schedulify.Contracts.Responses;

namespace Schedulify.Api.Mappings;

public class CategoryProfile: Profile
{
    public CategoryProfile()
    {
        CreateMap<CategoryBaseRequest, CategoryBaseDto>()
            .ForMemberNewGuid(dest => dest.Id)
            .ForMemberFromItem(dest => dest.OwnerId)
            .ForMemberDateTimeOffsetNow(dest => dest.UpdatedAt);
        
        CreateMap<CreateCategoryRequest, CreateCategoryDto>()
            .IncludeBase<CategoryBaseRequest, CategoryBaseDto>()
            .ForMemberNewGuid(desc => desc.Id);

        CreateMap<UpdateCategoryRequest, UpdateCategoryDto>()
            .IncludeBase<CategoryBaseRequest, CategoryBaseDto>()
            .ForMember(
                dest => dest.Id, opt => 
                    opt.MapFrom(src => src.Id
            ));
        
        CreateMap<CategoryEntity, CategoryModel>()
            .ReverseMap(); // MODEL NOT EXTENDED

        CreateMap<CategoryModel, CreateCategoryResponse>();
        CreateMap<CategoryModel, UpdateCategoryResponse>();
    }
}