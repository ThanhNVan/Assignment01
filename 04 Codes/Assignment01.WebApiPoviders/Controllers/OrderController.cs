using Assignment01.EntityProviders;
using Assignment01.LogicProviders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Assignment01.WebApiPoviders;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    #region [ Fields ]
    private readonly ILogger<OrderController> _logger;
    private readonly LogicContext _logicContext;
    #endregion

    #region [ CTor ]
    public OrderController(ILogger<OrderController> logger,
                                LogicContext logicContext) {
        this._logger = logger;
        this._logicContext = logicContext;
    }
    #endregion

    #region [ Methods - CRUD ]
    [HttpPost]
    public async Task<ActionResult<bool>> AddAsync([FromBody] Order order) {
        try {
            var dbEntity = await this._logicContext.Order.GetSingleByIdAsync(order.OrderId);
            if (dbEntity != null) {
                return BadRequest("Duplicated entity");
            }

            var result = await this._logicContext.Order.AddAsync(order);

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
    public async Task<ActionResult<List<Order>>> GetListAllAsync() {
        try {
            var result = await this._logicContext.Order.GetListAllAsync();
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
    public async Task<ActionResult<Order>> GetSingleByIdAsync(int id) {
        try {
            var result = await this._logicContext.Order.GetSingleByIdAsync(id);
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
    public async Task<ActionResult<bool>> UpdateAsync([FromBody] Order order) {
        try {
            var result = await this._logicContext.Order.UpdateAsync(order);

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
            var dbEntity = await this._logicContext.Order.GetSingleByIdAsync(id);

            if (dbEntity == null) {
                return BadRequest("Not existed entity");
            }

            var result = await this._logicContext.Order.DeleteAsync(dbEntity);

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
    [HttpGet("ByMemberId/{memberId}")]
    public async Task<ActionResult<List<Order>>> GetListByMemberIdAsync(int memberId) {
        try {

            var dbResult = await this._logicContext.Order.GetListByMemberIdAsync(memberId);
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
