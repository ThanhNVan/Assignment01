using Assignment01.EntityProviders;
using Assignment01.LogicProviders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Assignment01.WebApiPoviders;

[ApiController]
[Route("api/[controller]")]
public class OrderDetailController : ControllerBase {
    #region [ Fields ]
    private readonly ILogger<OrderDetailController> _logger;
    private readonly LogicContext _logicContext;
    #endregion

    #region [ CTor ]
    public OrderDetailController(ILogger<OrderDetailController> logger,
                                LogicContext logicContext) {
        this._logger = logger;
        this._logicContext = logicContext;
    }
    #endregion

    #region [ Methods - CRUD ]
    [HttpPost("add")]
    public async Task<ActionResult<bool>> AddAsync([FromBody] OrderDetail orderDetail) {
        try {
            var dbEntity = await this._logicContext.OrderDetail.GetSingleByOrderIdAndProductIdAsync(orderDetail.OrderId, orderDetail.ProductId);
            if (dbEntity != null) {
                return BadRequest("Duplicated entity");
            }

            var result = await this._logicContext.OrderDetail.AddAsync(orderDetail);

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
    public async Task<ActionResult<List<OrderDetail>>> GetListAllAsync() {
        try {
            var result = await this._logicContext.OrderDetail.GetListAllAsync();
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

    [HttpGet("single/{orderId}&&{productId}")]
    public async Task<ActionResult<OrderDetail>> GetSingleByIdAsync(int orderId, int productId) {
        try {
            var result = await this._logicContext.OrderDetail.GetSingleByOrderIdAndProductIdAsync(orderId, productId);
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

    [HttpPut("update")]
    public async Task<ActionResult<bool>> UpdateAsync([FromBody] OrderDetail OrderDetail) {
        try {
            var result = await this._logicContext.OrderDetail.UpdateAsync(OrderDetail);

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

    [HttpDelete("delete/{orderId}&&{productId}")]
    public async Task<ActionResult<bool>> DeleteAsync(int orderId, int productId) {
        try {
            var dbEntity = await this._logicContext.OrderDetail.GetSingleByOrderIdAndProductIdAsync(orderId, productId);

            if (dbEntity == null) {
                return BadRequest("Not existed entity");
            }

            var result = await this._logicContext.OrderDetail.DeleteAsync(dbEntity);

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
    [HttpGet("ByOrderId/{orderId}")]
    public async Task<ActionResult<List<OrderDetail>>> GetListByOrderIdAsync(int orderId) {
        try {
            var dbResult = await this._logicContext.OrderDetail.GetListByOrderIdAsync(orderId);

            return Ok(dbResult);
        } catch (ArgumentNullException ex) {
            this._logger.LogError(ex.Message);
            return BadRequest();
        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost("Report")]
    public async Task<ActionResult<List<OrderDetail>>> GetListByReportAsync([FromBody] List<DateTime> dateTimes) {
        try {
            var dbOrderList = await this._logicContext.Order.GetListByDateRangeAsync(dateTimes.FirstOrDefault(), dateTimes.LastOrDefault());
            var dbResult = await this._logicContext.OrderDetail.GetListByOrderListAsync(dbOrderList);

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
