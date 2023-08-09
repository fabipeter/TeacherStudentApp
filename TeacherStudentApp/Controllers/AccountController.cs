using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Application.Account;
using Application.Account.DTOs;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Application.Account.Register;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TeacherStudentApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : BaseApiController
    {
       
        [HttpPost("[action]")]
        [ProducesResponseType(400, Type = typeof(BaseResponse))]
        [ProducesResponseType(200, Type = typeof(BaseResponse))]
        public async Task<IActionResult> RegisterUser(Command registerUserCommand)
        {
            if (!ModelState.IsValid) return BadRequest();
            return HandleResult(await Mediator.Send(registerUserCommand));
        }
        protected ActionResult HandleResult(BaseResponse result)
        {
            if (result.IsSuccess) return Ok(result);
            if (!result.IsSuccess) return BadRequest(result);
            return BadRequest(result);
        }
    }
}

