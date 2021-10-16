using ChatApplication.Entities;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Data.Queries.UserQueries
{
    public class UserQueries : IUserQueries
    {
        private readonly string _connectionString;
        private IDbConnection Connection => new SqlConnection(_connectionString);
        public UserQueries(IOptions<ConnectionString> connectionStrings)
        {
            _connectionString = connectionStrings.Value.defaultConnection;
        }
        public async Task<User> GetUserAsync(Guid guid)
        {
            string sp = "GET_USER";
            using (var con = Connection)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", guid, dbType: DbType.Guid);
                var user = await con.QueryFirstOrDefaultAsync<User>(sp, param: parameters, commandType: CommandType.StoredProcedure);
                return user;
            }
        }

        public async Task<List<User>> GetUsersAsync()
        {
            string sp = "GET_USERS";
            using (var con = Connection)
            {
                var users = await con.QueryAsync<User>(sp, commandType: CommandType.StoredProcedure);
                return users.AsList();
            }
        }

        public async Task<User> GetUserAsync(string username, string password)
        {
            string sp = "AUTHENTICATE_USER";
            using (var con = Connection)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Username", username, dbType: DbType.String);
                parameters.Add("@Password", password, dbType: DbType.String);
                var user = await con.QueryFirstOrDefaultAsync<User>(sp,param:parameters, commandType: CommandType.StoredProcedure);
                if (user != null)
                {
                    var roles = await GetUserRolesAsync(user.UserId);
                    user.Roles = roles;
                }
                       
                return user;
            }
        }

        public async Task<List<Role>> GetUserRolesAsync(Guid userId)
        {
            string sp = "GET_USER_ROLES";
            using (var con = Connection)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserId", userId, dbType: DbType.Guid);
                var roles =await con.QueryAsync<Role>(sp, param: parameters, commandType: CommandType.StoredProcedure);
                return roles.ToList();
            }
        }

        public async Task<User> GetUserByUsername(string username)
        {
            string sp = "GET_USER_BY_NAME";
            using (var con = Connection)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Username", username, dbType: DbType.String);
                var user = await con.QueryFirstOrDefaultAsync<User>(sp, param: parameters, commandType: CommandType.StoredProcedure);
                return user;
            }
        }
    }
}
