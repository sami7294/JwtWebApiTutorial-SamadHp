using JwtWebApiTutorial.Entities;
using JwtWebApiTutorial.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JwtWebApiTutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailDapperController : ControllerBase
    {
        private readonly IUserDetailRepository _userDetailRepository;

        public UserDetailDapperController(IUserDetailRepository userDetailRepository)
        {
            _userDetailRepository = userDetailRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<UserDetails>> GetUserDetail()
        {
            return await _userDetailRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetails>> GetUserDetailById(int id)
        {
            return await _userDetailRepository.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult<UserDetails>> PostUserDetail([FromBody] UserDetails superHero)
        {
            var newUserDetail = await _userDetailRepository.Create(superHero);

            return CreatedAtAction(nameof(GetUserDetail), new { id = newUserDetail.Id }, newUserDetail);
        }

        [HttpPut]
        public async Task<ActionResult> PutUserDetail(int id, [FromBody] UserDetails superHero)
        {
            if (id != superHero.Id)
            {
                return BadRequest();
            }

            await _userDetailRepository.Update(superHero);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserDetail(int id)
        {
            var SuperHeroToDelete = await _userDetailRepository.Get(id);
            if (SuperHeroToDelete == null)
                return NotFound();

            await _userDetailRepository.Delete(SuperHeroToDelete.Id);
            return NoContent();
        }
    }
}
