using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Factory.API.Core.Contracts;
using Factory.API.Core.Models.ToolType;
using Factory.API.Core.Models.Extras;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.OData.Query;

namespace Factory.API.Controllers
{
    [Route("api/v{version:apiVersion}/ToolTypes")]
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    public class ToolTypesController : ControllerBase
    {
        private readonly IToolTypeRepository _context;

        public ToolTypesController(IToolTypeRepository context)
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<GetToolTypeDto>>> GetToolTypes()
        {
            return Ok(await _context.GetAllAsync<GetToolTypeDto>());
        }

        [HttpGet("GetPaged")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<GetToolTypeDto>>> GetPagedToolTypes([FromBody] QueryParameters parameters)
        {
            return Ok(await _context.GetAllAsync<GetToolTypeDto>(parameters));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ToolTypeDto>> GetToolType(int id)
        {
            var toolType = await _context.GetDetails(id);

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
                await _context.UpdateAsync<GetToolTypeDto>(id, updateToolTypeDto);
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
            var result = await _context.AddAsync<BaseToolTypeDto, GetToolTypeDto>(toolTypeDto);
            return CreatedAtAction(nameof(GetToolType), new { id = result.Id }, result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteToolType(int id)
        {
            await _context.DeleteAsyc(id);
            return NoContent();
        }

        private async Task<bool> ToolTypeExistsAsync(int id)
        {
            return await _context.Exists(id);
        }
    }
}
