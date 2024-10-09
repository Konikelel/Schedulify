using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Schedulify.App.Dtos;
using Schedulify.App.Services;
using Schedulify.Contracts.Requests;
using Schedulify.Contracts.Responses;

namespace Schedulify.Api.Controllers;

[ApiController]
public class CategoriesController: ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;
    private const string TempOwnerId = "00000000-0000-0000-0000-000000000000";

    public CategoriesController(
        ICategoryService categoryService,
        IMapper mapper)
    {
        _categoryService = categoryService;
        _mapper = mapper;
    }
    
    [HttpGet(ApiEndpoints.Categories.Get)]
    public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken token = default)
    {
        var model = await _categoryService.GetByIdAsync(id, token);
        
        if (model == null)
        {
            return NotFound();
        }
        
        var response = _mapper.Map<GetCategoryResponse>(model);
        return Ok(response);
    }
    
    [HttpGet(ApiEndpoints.Categories.GetByUser)]
    public async Task<IActionResult> GetByUser([FromQuery] Guid id, CancellationToken token = default)
    {
        var models = await _categoryService.GetByOwnerIdAsync(id, token);
        
        var response = _mapper.Map<GetMultipleCategoryResponse>(models);
        return Ok(response);
    }
    
    [HttpPost(ApiEndpoints.Categories.Create)]
    public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request, CancellationToken token = default)
    {
        var dto = _mapper.MapUsingItems<CreateCategoryDto>(request, new {OwnerId = TempOwnerId});
        var result = await _categoryService.CreateAsync(dto, token);

        if (!result)
        {
            return StatusCode(500, new { Message = "An unexpected error occurred while creating category." });
        }
        
        var response = _mapper.Map<CreateCategoryResponse>(dto);
        return CreatedAtAction(nameof(Get), new { id = dto.Id }, response);
    }
    
    [HttpPut(ApiEndpoints.Categories.Update)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCategoryRequest request, CancellationToken token = default)
    {
        var dto = _mapper.MapUsingItems<UpdateCategoryDto>(request, new {Id = id, OwnerId = TempOwnerId});
        var result = await _categoryService.UpdateAsync(dto, token);
        
        if (result == null)
        {
            return StatusCode(500, new { Message = "An unexpected error occurred while updating category." });
        }
        
        var response = _mapper.Map<UpdateCategoryResponse>(dto);
        return Ok(response);
    }
    
    [HttpDelete(ApiEndpoints.Categories.Delete)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken token = default)
    {
        var result = await _categoryService.DeleteByIdAsync(id, token);
        return result ? Ok() : NotFound();
    }
}