using FitnessTracker.BusinessLogic.Interfaces;
using FitnessTracker.BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FitnessTracker.WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UnitController : Controller
    {
        private readonly IUnitOfWork _uow;

        public UnitController(IUnitOfWork uow)
        {
            this._uow = uow;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllUnits()
        {
            IEnumerable<Unit> units = await _uow.UnitRepository.GetUnitsAsync();

            if (units.Any())
            {
                return Ok(units);
            }

            return BadRequest(units);
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> GetUnitById(int id)
        {
            Unit unit = await _uow.UnitRepository.GetUnitByIdAsync(id);

            if (unit != null)
            {
                return Ok(unit);
            }

            return BadRequest(unit);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> AddUnit(Unit unit)
        {
            await _uow.UnitRepository.AddUnitAsync(unit);
            await _uow.SaveAsync();

            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteUnit(int id)
        {
            await _uow.UnitRepository.DeleteUnitAsync(id);
            await _uow.SaveAsync();

            return Ok(id);
        }
    }
}
