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

namespace ChatApplication.Data.Users
{
    public class UserOperations : IUserOperations
    {
        private readonly string _connectionString;
        private IDbConnection Connection => new SqlConnection(_connectionString);
        public UserOperations(IOptions<ConnectionString> connectionStrings)
        {
            _connectionString = connectionStrings.Value.defaultConnection;
        }
        public void AddUser(User user)
        {
            string sp = "ADD_USER";
            using (var connection = Connection)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Username", user.Username, DbType.String);
                parameters.Add("@Email", user.Email, DbType.String);
                parameters.Add("@PasswordHash", user.PasswordHash, DbType.String);
                parameters.Add("@FileName", user.FileName, DbType.String);
                parameters.Add("@ProfilePicture", user.ProfilePicture, DbType.Binary);
                parameters.Add("@Biography", user.Biography, DbType.String);
                parameters.Add("@Birthday", user.Birthday, DbType.Date);
                parameters.Add("@LastActivityDate", user.LastActivityDate, DbType.DateTime);
                var affectedRows = connection.Execute(sp, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public void DeleteUser(Guid id)
        {
            string sp = "DELETE_USER";
            using (var connection = Connection)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, dbType: DbType.Guid);
                var affectedRows = connection.Execute(sp, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public User GetUser(Guid id)
        {
            string sp = "GET_USER";
            using (var connection = Connection)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id, dbType: DbType.Guid);
                var user = connection.QueryFirst<User>(sp, param: parameters, commandType: CommandType.StoredProcedure);
                return user;
            }
        }

        public List<User> GetUsers()
        {
            string sp = "GET_USERS";
            using (var connection = Connection)
            {
                var users = connection.Query<User>(sp, commandType: CommandType.StoredProcedure).AsList();
                return users;
            }
        }

        public void UpdateUser(User user)
        {
            string sp = "UPDATE_USER";
            using (var connection = Connection)
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Username", user.Username, DbType.String);
                parameters.Add("@Email", user.Email, DbType.String);
                parameters.Add("@PasswordHash", user.PasswordHash, DbType.String);
                parameters.Add("@FileName", user.FileName, DbType.String);
                parameters.Add("@ProfilePicture", user.ProfilePicture, DbType.Binary);
                parameters.Add("@Biography", user.Biography, DbType.String);
                parameters.Add("@Birthday", user.Birthday, DbType.Date);
                parameters.Add("@LastActivityDate", user.LastActivityDate, DbType.DateTime);
                parameters.Add("@Id", user.Id, dbType: DbType.Guid);

                var affectedRows = connection.Execute(sp, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
