using System.Net;
using System.Threading.Tasks;
using Kontabilize.Domain.CompanyContext.Commands.Inputs;
using Kontabilize.Domain.CompanyContext.Handlers;
using Kontabilize.Domain.CompanyContext.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kontabilize.Api.Controller
{
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class CompanyController : ControllerBase
    {
        private readonly CompanyHandler _companyHandler;
        private readonly CompanyService _companyService;

        public CompanyController(CompanyHandler companyHandler, CompanyService companyService)
        {
            _companyHandler = companyHandler;
            _companyService = companyService;
        }

        [HttpPost("new")]
        [ApiVersion("1.0")]
        [ProducesResponseType((int) HttpStatusCode.Created)]
        [ProducesResponseType((int) HttpStatusCode.NotAcceptable)]
        [AllowAnonymous]
        public async Task<IActionResult> CreateNewCompany([FromBody] CreateNewCompanyCommand command)
        {
            var result = await _companyHandler.Handler(command);
            return result.Success ? Ok(result) : StatusCode(406, result);
        }

        [HttpGet("new")]
        [ApiVersion("1.0")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [Authorize(Roles = "ACCOUNTANT,ADMIN")]
        public async Task<IActionResult> GetAllNewCompany()
        {
            var result = await _companyService.GetAllNewCompany();
            if (result.Success)
            {
                return Ok(result);
            }

            return NoContent();
        }

        [HttpGet("new/cpf/{cpf}")]
        [ApiVersion("1.0")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [AllowAnonymous]
        public async Task<IActionResult> GetNewCompanyByCpf(string cpf)
        {
            var result = await _companyService.GetNewCompanyByCpf(cpf);
            if (result.Success)
            {
                return Ok(result);
            }

            return NoContent();
        }

        [HttpGet("new/email/{email}")]
        [ApiVersion("1.0")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [AllowAnonymous]
        public async Task<IActionResult> GetNewCompanyByEmail(string email)
        {
            var result = await _companyService.GetNewCompanyByEmail(email);
            if (result.Success)
            {
                return Ok(result);
            }

            return NoContent();
        }

        [HttpPost("migration")]
        [ApiVersion("1.0")]
        [ProducesResponseType((int) HttpStatusCode.Created)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> MigrateNewCompany([FromBody] CreateMigrateCompanyCommand command)
        {
            var result = await _companyHandler.Handler(command);
            return result.Success ? Ok(result) : StatusCode(406, result);
        }

        [HttpGet("migration")]
        [ApiVersion("1.0")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [Authorize(Roles = "ACCOUNTANT,ADMIN")]
        public async Task<IActionResult> GetAllMigrationCompany()
        {
            var result = await _companyService.GetAllMigrationsCompany();
            return !result.Success ? StatusCode(204, result) : Ok(result);
        }

        [HttpGet("migration/cnpj/{cnpj}")]
        [ApiVersion("1.0")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [AllowAnonymous]
        public async Task<IActionResult> GetMigrateCompanyByCpf(string cnpj)
        {
            var result = await _companyService.GetMigrateCompanyByCnpj(cnpj);
            return result.Success ? Ok(result) : StatusCode(204, result);
        }

        [HttpGet("migration/email/{email}")]
        [ApiVersion("1.0")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [AllowAnonymous]
        public async Task<IActionResult> GetMigrateCompanyByEmail(string email)
        {
            var result = await _companyService.GetMigrateCompanyByEmail(email);
            return result.Success ? Ok(result) : StatusCode(204, result);
        }

        // [HttpGet("user/{id:Guid}")]
        // [ApiVersion("1.0")]
        // [ProducesResponseType((int) HttpStatusCode.Created)]
        // [ProducesResponseType((int) HttpStatusCode.NoContent)]
        // [Authorize(Roles = "ACCOUNTANT,ADMIN")]
        // public async Task<IActionResult> CreateUserByCompany(Guid id)
        // {
        //     var created = await _companyHandler.CreateUserByCompany(id);
        //     if (!created)
        //     {
        //         return NoContent();
        //     }
        //
        //     return StatusCode(201);
        // }
    }
}