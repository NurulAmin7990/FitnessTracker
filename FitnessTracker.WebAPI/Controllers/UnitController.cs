using FitnessTracker.BusinessLogic.Interfaces;
using FitnessTracker.BusinessLogic.Models;
using FitnessTracker.WebAPI.ViewModels;
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

        /// <summary>
        /// Get all units
        /// </summary>
        /// <returns>Existing units</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Unit/Get
        ///     [{
        ///        "id": 1,
        ///        "unitType": "KG",
        ///        "description": "Kilograms"
        ///     },
        ///     {
        ///        "id": 2,
        ///        "unitType": "G",
        ///        "description": "Grams"
        ///     }]
        ///
        /// </remarks>
        /// <response code="200">Returns all the existing units</response>
        /// <response code="204">No existing units</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("Get")]
        public async Task<IActionResult> GetUnits()
        {
            IEnumerable<Unit> units = await _uow.UnitRepository.GetUnitsAsync();

            if (units.Any())
            {
                IEnumerable<ViewUnitVM> UnitsVMs = new List<ViewUnitVM>();

                foreach (Unit unit in units)
                {
                    UnitsVMs.Append(new ViewUnitVM
                    {
                        Id = unit.Id,
                        UnitType = unit.UnitType,
                        Description = unit.Description
                    });
                }

                return Ok(UnitsVMs);
            }

            return NoContent();
        }

        /// <summary>
        /// Get single unit by it's unique identifier
        /// </summary>
        /// <returns>Existing unit</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Unit/Get/1
        ///     {
        ///        "id": 1,
        ///        "unitType": "KG",
        ///        "description": "Kilograms"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the existing unique unit</response>
        /// <response code="204">No existing unit found by the given identifier</response>
        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> GetUnitById(int id)
        {
            Unit unit = await _uow.UnitRepository.GetUnitByIdAsync(id);

            if (unit != null)
            {
                return Ok(new ViewUnitVM() { Id = unit.Id, UnitType = unit.UnitType, Description = unit.Description});
            }

            return NotFound(string.Format("Unit with unique identifier {0} does not exist.", id));
        }

        /// <summary>
        /// Create new unit to store in database
        /// </summary>
        /// <returns>Newly created unit</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Unit/Create
        ///     {
        ///        "unitType": "KG",
        ///        "description": "Kilograms"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the new unique unit record</response>
        /// <response code="500">Failed to create new unit record</response>
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> AddUnit(CreateUnitVM unit)
        {
            if (ModelState.IsValid)
            {
                await _uow.UnitRepository.AddUnitAsync(new Unit()
                {
                    UnitType = unit.UnitType,
                    Description = unit.Description,
                });
                await _uow.SaveAsync();

                return StatusCode((int)HttpStatusCode.Created);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ModelState);
            }
        }

        /// <summary>
        /// Removed existing unit record from database
        /// </summary>
        /// <returns>Nothing</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Unit/Delete/1
        ///
        /// </remarks>
        /// <response code="200">Removed the existing unit record from the database</response>
        /// <response code="500">Failed to remove the existing unit record from the database</response>
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
