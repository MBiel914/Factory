using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Factory.API.Service.Contracts;
using Factory.API.Service.DTOs.ToolType;
using Factory.API.Service.DTOs.Extras;
using Microsoft.AspNetCore.Authorization;

namespace Factory.API.Controllers
{
    [Route("api/v{version:apiVersion}/tool-types")]
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    public class ToolTypesController : ControllerBase
    {
        private readonly IToolTypeRepository _repository;

        public ToolTypesController(IToolTypeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<GetToolTypeDto>>> GetToolTypes()
        {
            return Ok(await _repository.GetAllAsync<GetToolTypeDto>());
        }

        [HttpGet("paged")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<GetToolTypeDto>>> GetPagedToolTypes([FromBody] QueryParametersDto parameters)
        {
            return Ok(await _repository.GetAllAsync<GetToolTypeDto>(parameters));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ToolTypeDto>> GetToolType(int id)
        {
            var toolType = await _repository.GetDetails(id);

            if (toolType == null)
            {
                return NotFound();
            }

            return Ok(toolType);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutToolType(int id, GetToolTypeDto updateToolTypeDto)
        {
            if (id != updateToolTypeDto.Id)
            {
                return BadRequest("Invalid Record ID");
            }

            try
            {
                await _repository.UpdateAsync<GetToolTypeDto>(id, updateToolTypeDto);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ToolTypeExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<GetToolTypeDto>> PostToolType(BaseToolTypeDto toolTypeDto)
        {
            var result = await _repository.AddAsync<BaseToolTypeDto, GetToolTypeDto>(toolTypeDto);
            return CreatedAtAction(nameof(GetToolType), new { id = result.Id }, result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteToolType(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }

        private async Task<bool> ToolTypeExistsAsync(int id)
        {
            return await _repository.ExistsAsync(id);
        }
    }
}
