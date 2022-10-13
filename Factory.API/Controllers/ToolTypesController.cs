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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToolType>>> GetToolTypes()
        {
            return await _context.GetAllAsync<ToolType>();
        }

        // GET: api/ToolTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToolType>> GetToolType(int id)
        {
            var toolType = await _context.GetAsync<ToolType>(id);

            if (toolType == null)
            {
                return NotFound();
            }

            return toolType;
        }

        // PUT: api/ToolTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToolType(int id, ToolType toolType)
        {
            if (id != toolType.Id)
            {
                return BadRequest();
            }

            //_context.Entry(toolType).State = EntityState.Modified;

            try
            {
                await _context.UpdateAsync<ToolType>(id, toolType);
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ToolType>> PostToolType(ToolType toolType)
        {
            //_context.ToolTypes.Add(toolType);
            //await _context.SaveChangesAsync();
            await _context.AddAsync<ToolType, ToolType>(toolType);
            return CreatedAtAction("GetToolType", new { id = toolType.Id }, toolType);
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
