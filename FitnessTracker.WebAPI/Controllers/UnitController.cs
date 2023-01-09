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
    [SwaggerTag("Create, update and delete Units")]
    public class UnitController : Controller
    {
        private readonly IUnitOfWork _uow;

        public UnitController(IUnitOfWork uow)
        {
            this._uow = uow;
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
