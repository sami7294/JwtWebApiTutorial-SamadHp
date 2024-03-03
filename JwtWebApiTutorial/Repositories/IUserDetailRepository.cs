using JwtWebApiTutorial.Entities;

namespace JwtWebApiTutorial.Repositories
{
    public interface IUserDetailRepository
    {
        Task<IEnumerable<UserDetails>> Get();
        Task<UserDetails> Get(int id);
        Task<UserDetails> Create(UserDetails superHero);
        Task Update(UserDetails superHero);
        Task Delete(int id);
    }
}
