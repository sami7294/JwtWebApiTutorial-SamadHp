using Dapper;
using JwtWebApiTutorial.Entities;
using System.Data.SqlClient;

namespace JwtWebApiTutorial.Repositories
{
    public class UserDetailRepository : IUserDetailRepository
    {
        private readonly string _connectionString;

        public UserDetailRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<UserDetails> Create(UserDetails userDetails)
        {
            var sqlQuery = "INSERT INTO UserDetailTb( Name, FirstName, LastName, Place) VALUES ( @Name, @FirstName, @LastName, @Place)";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, new
                {
                    userDetails.Name,
                    userDetails.FirstName,
                    userDetails.LastName,
                    userDetails.Place
                });

                return userDetails;
            }
        }

        public async Task Delete(int id)
        {
            var sqlQuery = "DELETE FROM UserDetailTb WHERE Id=@Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, new { Id = id });
            }
        }

        public async Task<IEnumerable<UserDetails>> Get()
        {
            var sqlQuery = "SELECT * FROM UserDetailTb";

            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<UserDetails>(sqlQuery);
            }
        }

        public async Task<UserDetails> Get(int id)
        {
            var sqlQuery = "SELECT * FROM UserDetailTb WHERE Id=@Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<UserDetails>(sqlQuery, new { Id = id });
            }
        }

        public async Task Update(UserDetails userDetails)
        {
            var sqlQuery = "UPDATE UserDetailTb SET Name=@Name, FirstName=@FirstName, LastName=@LastName, Place=@Place WHERE Id=@Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, new
                {
                    userDetails.Id,
                    userDetails.Name,
                    userDetails.FirstName,
                    userDetails.LastName,
                    userDetails.Place
                });
            }
        }

        
    }
}