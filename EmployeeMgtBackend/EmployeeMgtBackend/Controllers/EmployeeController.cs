﻿using EmployeeBusinessLayer.IEmployeeBusiness;
using EmployeeModel;
using EmployeeModelLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMgtBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeBusines employeeBusiness;
        private readonly IConfiguration configuration;

        public EmployeeController(IEmployeeBusines employeeBusines,IConfiguration configuration)
        {
            this.employeeBusiness = employeeBusines;
            this.configuration = configuration;
        }

        /// <summary>
        /// Register the Employee.
        /// </summary>
        /// <param name="create">The create.</param>
        /// <returns></returns>

        [HttpPost]
        public IActionResult RegisterEmployee([FromBody] EmployeeModels create)
        {
            try
            {
                var result = this.employeeBusiness.RegisterEmployee(create);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Employee Added Succesfully", Data = result });
                }
                return this.BadRequest(new { Status = true, Message = "Error While Adding Employee" });
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        /// <summary>
        /// Login User.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <returns></returns>

        [HttpPost]
        [Route("Login")]
        public IActionResult LoginUser([FromBody] LoginModel login)
        {
            try
            {
                var logins = this.employeeBusiness.LoginUser(login);
                if (logins != null)
                {
                    return this.Ok(new { Status = true, Message = "Login Success", Data = logins });
                }
                return this.NotFound(new { Status = false, Message = "Login Failed" });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = false, Message = e.Message });
            }
        }
    }
}
