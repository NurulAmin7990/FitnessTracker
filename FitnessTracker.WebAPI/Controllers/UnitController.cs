using FitnessTracker.BusinessLogic.Interfaces;
using FitnessTracker.BusinessLogic.Models;
using FitnessTracker.WebAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

namespace FitnessTracker.WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [SwaggerTag("Create, read, update and delete Units")]
    public class UnitController : Controller
    {
        private readonly IUnitOfWork _uow;

        public UnitController(IUnitOfWork uow)
        {
            this._uow = uow;
        }

        [HttpGet]
        [Route("Get")]
        [SwaggerOperation(
            Summary = "Get all units",
            Description = null,
            OperationId = "GetUnits",
            Tags = new[] { "Unit" }
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Returns all the existing units", type: typeof(List<ViewUnitVM>))]
        [SwaggerResponse(StatusCodes.Status204NoContent, "No existing units")]
        public async Task<IActionResult> GetUnits()
        {
            IEnumerable<Unit> units = await _uow.UnitRepository.GetUnitsAsync();

            if (units.Any())
            {
                List<ViewUnitVM> UnitsVMs = new List<ViewUnitVM>();

                foreach (Unit unit in units)
                {
                    UnitsVMs.Add(new ViewUnitVM
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

        [HttpGet]
        [Route("Get/{id}")]
        [SwaggerOperation(
            Summary = "Get single unit by it's unique identifier",
            Description = null,
            OperationId = "GetUnitById",
            Tags = new[] { "Unit" }
        )]
        [SwaggerResponse(StatusCodes.Status302Found, "Returns the existing unique unit", type: typeof(ViewUnitVM))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "No existing unit found by the given identifier")]
        public async Task<IActionResult> GetUnitById([SwaggerParameter("Unit Id", Required = true)] int id)
        {
            Unit unit = await _uow.UnitRepository.GetUnitByIdAsync(id);

            if (unit != null)
            {
                return StatusCode(StatusCodes.Status302Found, new ViewUnitVM()
                {
                    Id = unit.Id,
                    UnitType = unit.UnitType,
                    Description = unit.Description
                });
            }

            return NotFound(string.Format("Unit with unique identifier {0} does not exist.", id));
        }

        [HttpPost]
        [Route("Create")]
        [SwaggerOperation(
            Summary = "Create a new unit",
            Description = null,
            OperationId = "AddUnit",
            Tags = new[] { "Unit" }
        )]
        [SwaggerResponse(StatusCodes.Status201Created, "Returns the new unique unit record", type: typeof(ViewUnitVM))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Returns the model state dictionary", type: typeof(ModelStateDictionary))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Failed to create new unit record")]
        public async Task<IActionResult> AddUnit(CreateUnitVM unit)
        {
            if (ModelState.IsValid)
            {
                var Unit = await _uow.UnitRepository.AddUnitAsync(new Unit()
                {
                    UnitType = unit.UnitType,
                    Description = unit.Description,
                });

                await _uow.SaveAsync();

                return StatusCode(StatusCodes.Status201Created, new ViewUnitVM()
                {
                    Id = Unit.Id,
                    UnitType = Unit.UnitType,
                    Description = Unit.Description
                });
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        [SwaggerOperation(
            Summary = "Removed existing unit record",
            Description = null,
            OperationId = "DeleteUnit",
            Tags = new[] { "Unit" }
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Removed the existing unit record from the database", type: typeof(int))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Could not find the unit with the given unique identifier")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Failed to remove the existing unit record from the database")]
        public async Task<IActionResult> DeleteUnit([SwaggerParameter("Unit Id", Required = true)] int id)
        {
            if (await _uow.UnitRepository.GetUnitByIdAsync(id) != null)
            {
                await _uow.UnitRepository.DeleteUnitAsync(id);
                await _uow.SaveAsync();

                return Ok(id);
            }

            return NotFound(string.Format("Unit with unique identifier {0} does not exist.", id));
        }
    }
}
