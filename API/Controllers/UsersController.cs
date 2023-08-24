using API.Data;
using API.DTO_s;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        //private readonly DataContext _context;  //before creating a common repo for all user function

        public UsersController(IUserRepository userRepository, IMapper mapper) {
            this._userRepository = userRepository;
            this._mapper = mapper;
            // this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            //var users = await _context.Users.ToListAsync();

            /* below gave error as cannot convert ienumerable to action result ienumerable
            var users = await _userRepository.GetUsersAsync();
            return users;
            */

            /*
            var users = await _userRepository.GetUsersAsync();                  code before we used mapper in repository
            var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);*/

            var users = await _userRepository.GetMembersAsync();

            return Ok(users);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            //var user =  await _userRepository.GetUserByUsernameAsync(username);  code before we used mapper in repository
            //return _mapper.Map<MemberDto>(user);

            return await _userRepository.GetMemberAsync(username);

        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; //as now we  pass token (which has user) from our angular app
            var user = await _userRepository.GetUserByUsernameAsync(username);
            //EF is now tracking these changes so we can directly save at end

            if(user == null) { return NotFound(); }

            _mapper.Map(memberUpdateDto, user); //effectively updating the mentioned properties in memberdto

            if(await _userRepository.SaveAllAsync()) { return NoContent(); }

            return BadRequest("Failed to update user"); //will also be called if we make thw same req but with no 
            //changes in the properties, as there will be no chnages to be saved, but this is correct
        }

    }
}
