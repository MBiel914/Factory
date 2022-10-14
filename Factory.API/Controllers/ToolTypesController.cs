using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Factory.API.Data.Contexts;
using Factory.API.Data.Entities;
using Factory.API.Core.Contracts;
using Factory.API.Core.Models.ToolType;
using Factory.API.Core.Models.Extras;
using Microsoft.AspNetCore.Authorization;

namespace Factory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToolTypesController : ControllerBase
    {
        private readonly IToolTypeRepository _context;

        public ToolTypesController(IToolTypeRepository context)
        {
            _context = context;
        }

        // GET: api/ToolTypes
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<GetToolTypeDto>>> GetToolTypes()
        {
            return Ok(await _context.GetAllAsync<GetToolTypeDto>());
        }

        // GET: api/ToolTypes
        [HttpGet("GetPaged")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<GetToolTypeDto>>> GetPagedToolTypes([FromBody] QueryParameters parameters)
        {
            return Ok(await _context.GetAllAsync<GetToolTypeDto>(parameters));
        }

        // GET: api/ToolTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToolTypeDto>> GetToolType(int id)
        {
            var toolType = await _context.GetDetails(id);

            if (toolType == null)
            {
                return NotFound();
            }

            return Ok(toolType);
        }

        // PUT: api/ToolTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToolType(int id, GetToolTypeDto updateToolType)
        {
            if (id != updateToolType.Id)
            {
                return BadRequest("Invalid Record ID");
            }

            try
            {
                await _context.UpdateAsync<GetToolTypeDto>(id, updateToolType);
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

        // POST: api/ToolTypes
        [HttpPost]
        public async Task<ActionResult<GetToolTypeDto>> PostToolType(BaseToolTypeDto toolType)
        {
            var result = await _context.AddAsync<BaseToolTypeDto, GetToolTypeDto>(toolType);
            return CreatedAtAction(nameof(GetToolType), new { id = result.Id }, result);
        }

        // DELETE: api/ToolTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToolType(int id)
        {
            //var toolType = await _context.ToolTypes.FindAsync(id);
            //if (toolType == null)
            //{
            //    return NotFound();
            //}

            //_context.ToolTypes.Remove(toolType);
            //await _context.SaveChangesAsync();


            await _context.DeleteAsyc(id);
            return NoContent();
        }

        private async Task<bool> ToolTypeExistsAsync(int id)
        {
            return await _context.Exists(id);
        }
    }
}
