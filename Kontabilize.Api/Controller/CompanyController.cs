using System;
using System.Net;
using System.Threading.Tasks;
using Kontabilize.Api.Params;
using Kontabilize.Domain.CompanyContext.Commands.Inputs;
using Kontabilize.Domain.CompanyContext.Handlers;
using Kontabilize.Domain.CompanyContext.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kontabilize.Api.Controller
{
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = "Accountant,Admin")]
    public class CompanyController : ControllerBase
    {
        private readonly CompanyHandler _companyHandler;
        private readonly ICompanyService _companyService;

        public CompanyController(CompanyHandler companyHandler, ICompanyService companyService)
        {
            _companyHandler = companyHandler;
            _companyService = companyService;
        }

        [HttpGet]
        [ApiVersion("1.0")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        public async Task<IActionResult> GetAll([FromQuery] PageableParam param)
        {
            var result = await _companyService.GetAll(param.PageNumber, param.PageSize);
            if (result.Success)
            {
                return Ok(result);
            }

            return NoContent();
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
        public async Task<IActionResult> GetAllNewCompany([FromQuery] PageableParam param)
        {
            var result = await _companyService.GetAllNewCompany(param.PageNumber, param.PageSize);
            if (result.Success)
            {
                return Ok(result);
            }

            return NoContent();
        }
        
        [HttpDelete("{id:Guid}")]
        [ApiVersion("1.0")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            var result = await _companyService.DeleteCompany(id);
            if (result)
            {
                return Accepted();
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

        [HttpPost("migrate")]
        [ApiVersion("1.0")]
        [ProducesResponseType((int) HttpStatusCode.Created)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> MigrateNewCompany([FromBody] CreateMigrateCompanyCommand command)
        {
            var result = await _companyHandler.Handler(command);
            return result.Success ? Ok(result) : StatusCode(406, result);
        }

        [HttpGet("migrate")]
        [ApiVersion("1.0")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        public async Task<IActionResult> GetAllMigrationCompany([FromQuery] PageableParam param)
        {
            var result = await _companyService.GetAllMigrationsCompany(param.PageNumber, param.PageSize);
            return !result.Success ? StatusCode(204, result) : Ok(result);
        }

        [HttpGet("migrate/cnpj/{cnpj}")]
        [ApiVersion("1.0")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [AllowAnonymous]
        public async Task<IActionResult> GetMigrateCompanyByCpf(string cnpj)
        {
            var result = await _companyService.GetMigrateCompanyByCnpj(cnpj);
            return result.Success ? Ok(result) : StatusCode(204, result);
        }

        [HttpGet("migrate/email/{email}")]
        [ApiVersion("1.0")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [AllowAnonymous]
        public async Task<IActionResult> GetMigrateCompanyByEmail(string email)
        {
            var result = await _companyService.GetMigrateCompanyByEmail(email);
            return result.Success ? Ok(result) : StatusCode(204, result);
        }
        
    }
}