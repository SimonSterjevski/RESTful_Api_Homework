using Dapper;
using Dapper.Contrib.Extensions;
using SEDC.NotesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SEDC.NotesApp.DataAccess.Dapper
{
    public class UserDapperRepository: IRepository<User>
    {
        private string _connectionString;
        public UserDapperRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void Add(User entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                sqlConnection.Insert<User>(entity);
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                string sql = "DELETE FROM User WHERE Id = @Id";
                sqlConnection.Execute(sql, new { Id = id });
            }
        }

        public List<User> GetAll()
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                List<User> users = sqlConnection.Query<User>("SELECT * FROM User").ToList();
                return users;
            }
        }

        public User GetById(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                User user = sqlConnection.Query<User>("SELECT * FROM User WHERE Id = @Id", new { Id = id }).FirstOrDefault();
                return user;
            }
        }

        public void Update(User entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                sqlConnection.Update<User>(entity);
            }
        }
    }
}
