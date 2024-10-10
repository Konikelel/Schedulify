using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Schedulify.App.Dtos;
using Schedulify.App.Repositories;
using Schedulify.App.Services;
using Schedulify.Contracts.Requests;
using Schedulify.Contracts.Responses;

namespace Schedulify.Api.Controllers;

[ApiController]
public class SchedulesController: ControllerBase
{
    private readonly IScheduleService _scheduleService;
    private readonly IMapper _mapper;
    private const string TempOwnerId = "00000000-0000-0000-0000-000000000000";

    SchedulesController(IScheduleService scheduleService,
        IMapper mapper)
    {
        _scheduleService = scheduleService;
        _mapper = mapper;
    }
    
    [HttpGet(ApiEndpoints.Schedules.GetById)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken token = default)
    {
        var model = await _scheduleService.GetByIdAsync(id, token);

        if (model == null)
        {
            return NotFound();
        }
        
        var response = _mapper.Map<GetScheduleResponse>(model);
        return Ok(response);
    }
    
    [HttpGet(ApiEndpoints.Schedules.GetByUser)]
    public async Task<IActionResult> GetByUserId([FromRoute] Guid userId, CancellationToken token = default)
    {
        var models = await _scheduleService.GetByOwnerIdAsync(userId, token);

        var response = _mapper.Map<GetMultipleScheduleResponse>(models);
        return Ok(response);
    }
    
    [HttpPost(ApiEndpoints.Schedules.Create)]
    public async Task<IActionResult> Create([FromBody] CreateScheduleRequest request, CancellationToken token = default)
    {
        var dto = _mapper.MapUsingItems<CreateScheduleDto>(request, new {OwnerId = TempOwnerId});
        var result = await _scheduleService.CreateAsync(dto, token);
        
        if (!result)
        {
            return StatusCode(500, new { Message = "An unexpected error occurred while creating schedule." });
        }
        
        var response = _mapper.Map<CreateScheduleResponses>(dto);
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, response);
    }
    
    [HttpPut(ApiEndpoints.Schedules.Update)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateScheduleRequest request, CancellationToken token = default)
    {
        var dto = _mapper.MapUsingItems<UpdateScheduleDto>(request, new {OwnerId = TempOwnerId});
        var model = await _scheduleService.UpdateAsync(dto, token);
        
        if (model == null)
        {
            return StatusCode(500, new { Message = "An unexpected error occurred while updating category." });
        }
        
        var response = _mapper.Map<UpdateCategoryResponse>(model);
        return Ok(response);
    }
    
    [HttpDelete(ApiEndpoints.Schedules.Delete)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken token = default)
    {
        var result = await _scheduleService.DeleteByIdAsync(id, token);
        
        return result ? NoContent() : NotFound();
    }
    
}