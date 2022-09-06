using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RestSharp;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Veterinary.Domain.Abstractions;
using Veterinary.Domain.Models;
using Veterinary.Domain.Models.API;

namespace VeterinaryAPI.Controllers
{
    [Route("api/auth/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpGet("test"),Authorize]
        public string Test()
        {
            return "authorized return from api/test";
        }

        [HttpPost("register")]
        public JsonResult Register(VetRegister request)
        {
            Vet vet = _service.Create(request).Result;
            if (vet == null)
            {
                return new JsonResult(new { Success = false, Message = "Vet is null." });
            }
            string token = _service.CreateToken(vet);
            return new JsonResult(new { Success = true, Message = token });
        }

        [HttpPost("login")]
        public JsonResult Login(VetLogin request)
        {
            Vet vet = _service.FindVetByEmail(request.Email);
            if (vet == null)
            {
                return new JsonResult(new { Success = false, Message = "User not found" });
            }

            if (!_service.VerifyPasswordHash(request.Password, vet))
            {
                return new JsonResult(new { Success = false, Message = "Password incorrect" });
            }

            string token = _service.CreateToken(vet);

            return new JsonResult(new { Success = true, Message = token });
        }
    }
}
