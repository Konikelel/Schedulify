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
        CreateMap<CreateCategoryRequest, CreateCategoryDto>()
            .ForMemberNewGuid(desc => desc.Id)
            .ForMemberFromItem(dest => dest.OwnerId, "OwnerId");

        CreateMap<UpdateCategoryRequest, UpdateCategoryDto>()
            .ForMemberFromItem(dest => dest.OwnerId, "OwnerId");
        
        CreateMap<CategoryEntity, CategoryModel>()
            .ReverseMap(); // MODEL NOT EXTENDED

        CreateMap<CategoryModel, CreateCategoryResponse>();
        CreateMap<CategoryModel, UpdateCategoryResponse>();
    }
}