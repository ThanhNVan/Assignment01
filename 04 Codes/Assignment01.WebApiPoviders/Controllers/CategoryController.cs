using Assignment01.EntityProviders;
using Assignment01.LogicProviders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Assignment01.WebApiPoviders.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase {
    #region [ Fields ]
    private readonly ILogger<CategoryController> _logger;
    private readonly LogicContext _logicContext;
    #endregion

    #region [ CTor ]
    public CategoryController(ILogger<CategoryController> logger,
                                LogicContext logicContext) {
        this._logger = logger;
        this._logicContext = logicContext;
    }
    #endregion

    #region [ Methods - CRUD ]
    [HttpPost]
    public async Task<ActionResult<bool>> AddAsync([FromBody] Category category) {
        try {
            var dbEntity = await this._logicContext.Category.GetSingleByIdAsync(category.CategoryId);
            if (dbEntity != null) {
                return BadRequest("Duplicated entity");
            }

            var result = await this._logicContext.Category.AddAsync(category);

            if (result) {
                return Ok("Added");
            } else {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

        } catch (ArgumentNullException ex) {
            this._logger.LogError(ex.Message);
            return BadRequest();
        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<Category>>> GetListAllAsync() {
        try {
            var result = await this._logicContext.Category.GetListAllAsync();
            if (result.Count > 0) {
                return Ok(result);
            }
            return BadRequest("Empty");

        } catch (ArgumentNullException ex) {
            this._logger.LogError(ex.Message);
            return BadRequest();
        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("single/{id}")]
    public async Task<ActionResult<Category>> GetSingleByIdAsync(int id) {
        try {
            var result = await this._logicContext.Category.GetSingleByIdAsync(id);
            if (result == null) {
                return BadRequest("Empty");
            }

            return Ok(result);
        } catch (ArgumentNullException ex) {
            this._logger.LogError(ex.Message);
            return BadRequest();
        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut]
    public async Task<ActionResult<bool>> UpdateAsync([FromBody] Category category) {
        try {
            var result = await this._logicContext.Category.UpdateAsync(category);

            if (result) {
                return Ok("Updated");
            }

            return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        } catch (ArgumentNullException ex) {
            this._logger.LogError(ex.Message);
            return BadRequest();
        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<bool>> DeleteAsync(int id) {
        try {
            var dbEntity = await this._logicContext.Category.GetSingleByIdAsync(id);

            if (dbEntity == null) {
                return BadRequest("Not existed entity");
            } 

            var result = await this._logicContext.Category.DeleteAsync(dbEntity);

            if (result) {
                return Ok("Deleted");
            }

            return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        } catch (ArgumentNullException ex) {
            this._logger.LogError(ex.Message);
            return BadRequest();
        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
    #endregion
}
