using Assignment01.EntityProviders;
using Assignment01.LogicProviders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Assignment01.WebApiPoviders;

[ApiController]
[Route("api/[controller]")]
public class MemberController : ControllerBase
{
    #region [ Fields ]
    private readonly ILogger<MemberController> _logger;
    private readonly LogicContext _logicContext;
    private readonly Admin _admin;
    #endregion

    #region [ CTor ]
    public MemberController(ILogger<MemberController> logger,
                                LogicContext logicContext,
                                Admin admin) {
        this._logger = logger;
        this._logicContext = logicContext;
        this._admin = admin;
    }
    #endregion

    #region [ Methods - CRUD ]
    [HttpPost("add")]
    public async Task<ActionResult<bool>> AddAsync([FromBody] Member Member) {
        try {
            var dbEntity = await this._logicContext.Member.GetSingleByIdAsync(Member.MemberId);
            if (dbEntity != null) {
                return BadRequest("Duplicated entity");
            }

            var result = await this._logicContext.Member.AddAsync(Member);

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
    public async Task<ActionResult<List<Member>>> GetListAllAsync() {
        try {
            var result = await this._logicContext.Member.GetListAllAsync();
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
    public async Task<ActionResult<Member>> GetSingleByIdAsync(int id) {
        try {
            var result = await this._logicContext.Member.GetSingleByIdAsync(id);
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
    
    [HttpGet("ByEmail/{email}")]
    public async Task<ActionResult<Member>> GetSingleByEmailAsync(string email) {
        try {
            var result = await this._logicContext.Member.GetSingleByEmailAsync(email);
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
    public async Task<ActionResult<bool>> UpdateAsync([FromBody] Member Member) {
        try {
            var result = await this._logicContext.Member.UpdateAsync(Member);

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
            var dbEntity = await this._logicContext.Member.GetSingleByIdAsync(id);

            if (dbEntity == null) {
                return BadRequest("Not existed entity");
            }

            var result = await this._logicContext.Member.DeleteAsync(dbEntity);

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

    #region [ Methods - Login ]
    [HttpPost("login")]
    public async Task<ActionResult<Admin>> LoginAsync([FromBody] Admin admin) {
        try {

            if (admin.Email.IsNullOrEmpty() || admin.Password.IsNullOrEmpty()) {
                return BadRequest("Empty Email or Password");
            }

            var isAdmin = this.IsAdminLogin(admin.Email, admin.Password);
            if (isAdmin) {
                return Ok(AppRole.Admin);
            }

            var dbEntity = await this._logicContext.Member.LoginAsync(admin.Email, admin.Password);
            if (dbEntity == null) {
                return BadRequest("Wrong Email or Password");
            }
            return Ok(dbEntity);


        } catch (ArgumentNullException ex) {
            this._logger.LogError(ex.Message);
            return BadRequest();
        } catch (Exception ex) {
            this._logger.LogError(ex.Message);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
    #endregion

    #region [ Methods - Private ]
    private bool IsAdminLogin(string email, string password) {
        var result = false;

        if (email == this._admin.Email && 
            password == this._admin.Password) {
            return true;
        }

        return result;
    }

    #endregion
}
