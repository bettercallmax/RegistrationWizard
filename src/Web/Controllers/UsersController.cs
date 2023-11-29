using Api.Controllers.Abstract;
using Api.Interfaces;
using Api.Models.User;
using Application.Repositories;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("user")]
    public class UsersController : ApiController
    {
        private readonly IIdentityService _identityService;
        private readonly IProvinceRepository _provinceRepository;
        private readonly IJwtService _jwtService;

        public UsersController(
            IIdentityService identityService,
            IJwtService jwtService,
            ILogger<UsersController> logger,
            IProvinceRepository provinceRepository) : base(logger)
        {
            _identityService = identityService;
            _jwtService = jwtService;
            _provinceRepository = provinceRepository;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<UserModel>> LoginAsync(UserLoginModel request, CancellationToken token)
        {
            var user = await _identityService.GetUserAsync(request.Login, token);
            if (user == null)
            {
                return NotFound();
            }

            if (!await _identityService.CheckPasswordAsync(request.Login, request.Password, token))
            {
                return Unauthorized();
            }

            return UserModel.FromDomain(user, token: _jwtService.CreateToken(user));
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<UserModel>> Register(RegisterModel request, CancellationToken token)
        {
            var province = await _provinceRepository.GetAsync(request.ProvinceId, token);
            if (province == null)
            {
                return NotFound($"Province with Id {request.ProvinceId} not found");
            }

            await _identityService.CreateUserAsync(request.Login, request.Password, province, token);
            var user = await _identityService.GetUserAsync(request.Login, token);

            return UserModel.FromDomain(user!, token: _jwtService.CreateToken(user!));
        }
    }
}
