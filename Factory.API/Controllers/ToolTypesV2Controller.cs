using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Factory.API.Core.Contracts;
using Factory.API.Core.Models.ToolType;
using Factory.API.Core.Models.Extras;
using Microsoft.AspNetCore.Authorization;
using Factory.API.Core.Exceptions;

namespace Factory.API.Controllers
{
    [Route("api/v{version:apiVersion}/tool-types")]
    [ApiController]
    [ApiVersion("2.0")]
    public class ToolTypesV2Controller : ControllerBase
    {
        private readonly IToolTypeRepository _repository;

        public ToolTypesV2Controller(IToolTypeRepository repository)
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
        public async Task<ActionResult<IEnumerable<GetToolTypeDto>>> GetPagedToolTypes([FromBody] QueryParameters parameters)
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
                await _repository.Update<GetToolTypeDto>(id, updateToolTypeDto);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
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
            try
            {
                await _repository.Delete(id);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return NoContent();
        }

        private async Task<bool> ToolTypeExistsAsync(int id)
        {
            return await _repository.Exists(id);
        }
    }
}
