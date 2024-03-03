using JwtWebApiTutorial.Data;
using JwtWebApiTutorial.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JwtWebApiTutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailController : ControllerBase
    {

        private readonly DataContext _context;
        public UserDetailController(DataContext context)
        {

            _context = context;

        }

        //[HttpGet,Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<UserDetails>>> GetUserDetail()
        {
            return Ok(await _context.UserDetailTb.ToListAsync());
        }

        [HttpGet("{id}"),Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserDetails>> GetUserDetail(int id)
        {
            var UserDetail = await _context.UserDetailTb.FindAsync(id);
            if (UserDetail == null)
            {
                return BadRequest("hero not find in here");
            }
            return Ok(UserDetail);
        }

        [HttpPost]
        public async Task<ActionResult<List<UserDetails>>> AddUserDetail(UserDetails userDetails)
        {
            _context.UserDetailTb.Add(userDetails);
            await _context.SaveChangesAsync();

            return Ok(await _context.UserDetailTb.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<UserDetails>>> UpdateUserDetail(UserDetails request)
        {
            var UserDetails = await _context.UserDetailTb.FindAsync(request.Id);
            if (UserDetails == null)
            {
                return BadRequest("hero not find in here");
            }

            UserDetails.Name = request.Name;
            UserDetails.FirstName = request.FirstName;
            UserDetails.LastName = request.LastName;
            UserDetails.Place = request.Place;

            await _context.SaveChangesAsync();

            return Ok(await _context.UserDetailTb.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserDetails>> DeleteUserDetail(int id)
        {
            var UserDetails = await _context.UserDetailTb.FindAsync(id);
            if (UserDetails == null)
            {
                return BadRequest("hero not find in here");
            }
            _context.UserDetailTb.Remove(UserDetails);

            await _context.SaveChangesAsync();

            return Ok(await _context.UserDetailTb.ToListAsync());
        }
    }
}
