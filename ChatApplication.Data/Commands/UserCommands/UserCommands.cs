using ChatApplication.Entities;
using ChatApplication.Entities.DTO;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApplication.Data.Commands.UserCommands
{
    public class UserCommands : IUserCommands
    {
        private readonly string _connectionString;
        private IDbConnection Connection => new SqlConnection(_connectionString);
        public UserCommands(IOptions<ConnectionString> connectionStrings)
        {
            _connectionString = connectionStrings.Value.defaultConnection;
        }
        public async Task AddUserAsync(User user)
        {
            string sp = "ADD_USER";
            using (var con = Connection)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Username", user.Username, DbType.String);
                parameters.Add("@Email", user.Email, DbType.String);
                parameters.Add("@PasswordHash", user.PasswordHash, DbType.String);
                parameters.Add("@Biography", user.Biography, DbType.String);
                parameters.Add("@Birthday", user.Birthday, DbType.Date);
                var affectedRows = await con.ExecuteAsync(sp, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteUserAsync(Guid guid)
        {
            string sp = "DELETE_USER";
            using (var con = Connection)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", guid, dbType: DbType.Guid);
                var affectedRows = await con.ExecuteAsync(sp, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            string sp = "UPDATE_USER";
            using (var con = Connection)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Username", user.Username, DbType.String);
                parameters.Add("@Email", user.Email, DbType.String);
                parameters.Add("@PasswordHash", user.PasswordHash, DbType.String);
                parameters.Add("@Biography", user.Biography, DbType.String);
                parameters.Add("@Birthday", user.Birthday, DbType.Date);
                parameters.Add("@LastActivityDate", user.LastActivityDate, DbType.DateTime);
                parameters.Add("@Id", user.UserId, dbType: DbType.Guid);

                var affectedRows = await con.ExecuteAsync(sp, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<int> ChangePasswordAsync(ChangePasswordDTO user)
        {
            string sp = "ACCOUNT_ChangePassword";
            using (var con = Connection)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Username", user.userName, DbType.String);
                parameters.Add("@OldPassword", user.oldPassword, DbType.String);
                parameters.Add("@NewPassword", user.newPassword, DbType.String);
                var affectedRows = await con.ExecuteAsync(sp, parameters, commandType: CommandType.StoredProcedure);
                return affectedRows;
            }     
        }
    }
}
