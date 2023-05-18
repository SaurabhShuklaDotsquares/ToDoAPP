using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Text;
using System;
using ToDo.Core.Entites;
using ToDo.Core.Interface;
using ToDo.WebAPI.Configs;
using ToDo.WebAPI.DTO;

namespace ToDo.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]    
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly JwtConfig _jwtConfig;

        public UserController(ILogger<UserController> logger, IUnitOfWork unitOfWork, IMapper mapper, IOptions<JwtConfig> jwtOptionConfig)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtConfig = jwtOptionConfig.Value;
        }
        private string GenerateToken(User user)
        {
            var authClaim = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.Sid,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Secret));
            var token = new JwtSecurityToken(
                        issuer: _jwtConfig.Issuer,
                        audience: _jwtConfig.Audience,
                        expires: DateTime.Now.AddMinutes(10),
                        claims: authClaim,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost]
        public IActionResult Register([FromForm] UserRegistrationDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation("Invalid User Object sent from client");
                    return BadRequest();
                }
                var userEntity = _mapper.Map<User>(model);
                if (model.Photo != null && model.Photo.Length > 0)
                {
                    var fileName = $"{DateTime.UtcNow.Ticks}-{model.Photo.FileName}";                 
                    var savedPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Photos", fileName);
                    using (var stream = new FileStream(savedPath, FileMode.Create))
                    {
                        model.Photo.CopyTo(stream);
                        userEntity.Photo = fileName;
                    }
                }
                _unitOfWork.User.CreateUser(userEntity);
                _unitOfWork.Save();
                _logger.LogInformation($"User with id : {userEntity.Id} has been Created Successfully");
                var userDto = _mapper.Map<UserDto>(userEntity);
                userDto.Token = GenerateToken(userEntity);
                return StatusCode(201, userDto);
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong inside Register(UserRegistrationDto model) action : {e.GetBaseException().Message}");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost]
        public IActionResult Login(UserLoginDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogInformation("Invalid User Object sent from client");
                    return BadRequest();
                }
                var userEntity = _unitOfWork.User.GetUserByEmailandPwd(model.Email, model.Password);
                if (userEntity == null)
                {
                    _logger.LogInformation("Invalid User Object sent from client");
                    return BadRequest();
                }
                var userDto = _mapper.Map<UserDto>(userEntity);
                userDto.Token = GenerateToken(userEntity);
                return StatusCode(201, userDto);
            }
            catch (Exception e)
            {
                _logger.LogError($"Something went wrong inside Register(UserRegistrationDto model) action : {e.GetBaseException().Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
