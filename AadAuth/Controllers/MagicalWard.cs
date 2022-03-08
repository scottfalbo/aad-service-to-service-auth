using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AadAuth.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Policy = "ValidateTokenPolicy")]
    [ApiController]
    [Route("[controller]")]
    public class MagicalWard : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "You disabled the magical wards and may enter!";
        }
    }
}
