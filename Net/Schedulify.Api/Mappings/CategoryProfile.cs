using AutoMapper;
using Schedulify.App.Dtos;
using Schedulify.App.Entities;
using Schedulify.App.Models;
using Schedulify.Contracts.Requests;
using Schedulify.Contracts.Responses;

namespace Schedulify.Api.Mappings;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CreateCategoryRequest, CreateCategoryDto>()
            .ForMemberNewGuid(dest => dest.Id)
            .ForMemberFromItem(dest => dest.OwnerId)
            .ForMemberDateTimeOffsetNow(dest => dest.CreatedAt)
            .ForMemberDateTimeOffsetNow(dest => dest.UpdatedAt);
        CreateMap<UpdateCategoryRequest, UpdateCategoryDto>()
            .ForMemberFromItem(dest => dest.Id)
            .ForMemberFromItem(dest => dest.OwnerId)
            .ForMemberDateTimeOffsetNow(dest => dest.UpdatedAt);
        
        CreateMap<CategoryEntity, CategoryModel>()
            .ReverseMap();
        CreateMap<IEnumerable<CategoryModel>, GetMultipleCategoryResponse>()
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src));
        
        CreateMap<CategoryModel, GetCategoryResponse>();
        CreateMap<CategoryModel, CreateCategoryResponse>();
        CreateMap<CategoryModel, UpdateCategoryResponse>();
    }
}