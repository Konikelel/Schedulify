using AutoMapper;
using Schedulify.App.Entities;
using Schedulify.App.Models;
using Schedulify.Contracts.Responses;

namespace Schedulify.API.Mappings;

public class CategoryProfile: Profile
{
    public CategoryProfile()
    {
        CreateMap<CategoryEntity, CategoryModel>(); //NOT EXTENDED
        
        CreateMap<CategoryModel, CreateCategoryResponse>();
        CreateMap<CategoryModel, UpdateCategoryResponse>();
    }
}