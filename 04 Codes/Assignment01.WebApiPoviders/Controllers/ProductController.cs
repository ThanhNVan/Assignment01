using Assignment01.EntityProviders;
using Assignment01.LogicProviders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Assignment01.WebApiPoviders;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    #region [ Fields ]
    private readonly ILogger<ProductController> _logger;
    private readonly LogicContext _logicContext;
    #endregion

    #region [ CTor ]
    public ProductController(ILogger<ProductController> logger,
                                LogicContext logicContext) {
        this._logger = logger;
        this._logicContext = logicContext;
    }
    #endregion

    #region [ Methods - CRUD ]
    [HttpPost]
    public async Task<ActionResult<bool>> AddAsync([FromBody] Product product) {
        try {
            var dbEntity = await this._logicContext.Product.GetSingleByIdAsync(product.ProductId);
            if (dbEntity != null) {
                return BadRequest("Duplicated entity");
            }

            var result = await this._logicContext.Product.AddAsync(product);

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
    public async Task<ActionResult<List<Product>>> GetListAllAsync() {
        try {
            var result = await this._logicContext.Product.GetListAllAsync();
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
    public async Task<ActionResult<Product>> GetSingleByIdAsync(int id) {
        try {
            var result = await this._logicContext.Product.GetSingleByIdAsync(id);
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
    public async Task<ActionResult<bool>> UpdateAsync([FromBody] Product product) {
        try {
            var result = await this._logicContext.Product.UpdateAsync(product);

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
            var dbEntity = await this._logicContext.Product.GetSingleByIdAsync(id);

            if (dbEntity == null) {
                return BadRequest("Not existed entity");
            }

            var result = await this._logicContext.Product.DeleteAsync(dbEntity);

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

    #region [ Methods -  ]
    [HttpGet("ByCategoryId/{categoryId}")]
    public async Task<ActionResult<List<Product>>> GetListByCategoryIdAsync(int categoryId) {
        try {
            var dbResult = await this._logicContext.Product.GetListByCategoryIdAsync(categoryId);

            return Ok(dbResult);
        } catch (ArgumentNullException ex) {
            this._logger.LogError(ex.Message);
            return BadRequest();
        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("Search/{searchString}")]
    public async Task<ActionResult<List<Product>>> GetListBySearchStringAsync(string searchString) {
        try {

            var dbResult = await this._logicContext.Product.GetListBySearchStringAsync(searchString);

            return Ok(dbResult);

        } catch (ArgumentNullException ex) {
            this._logger.LogError(ex.Message);
            return BadRequest();
        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("Search/{fromPrice}&&{toPrice}")]
    public async Task<ActionResult<List<Product>>> GetListByUnitPriceRangeAsync(decimal fromPrice, decimal toPrice) {
        try {
            var dbResult = await this._logicContext.Product.GetListByUnitPriceRangeAsync(fromPrice, toPrice);

            return Ok(dbResult);
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
