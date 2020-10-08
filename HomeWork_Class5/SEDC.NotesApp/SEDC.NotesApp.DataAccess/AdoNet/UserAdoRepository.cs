using SEDC.NotesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SEDC.NotesApp.DataAccess.AdoNet
{
    public class UserAdoRepository : IRepository<User>
    {
        private string _connectionString;

        public UserAdoRepository(string conString)
        {
            _connectionString = conString;
        }
        public void Add(User entity)
        {
            using (SqlConnection sqlCon = new SqlConnection(_connectionString))
            {
                sqlCon.Open();
                SqlCommand sqlCom = new SqlCommand();
                sqlCom.Connection = sqlCon;
                sqlCom.CommandText = "INSERT INTO User (FirstName, LastName, Username, Age) VALUES (@firstName, @lastName, @userName, @age)";
                sqlCom.Parameters.AddWithValue("@firstName", entity.FirstName);
                sqlCom.Parameters.AddWithValue("@lastName", entity.LastName);
                sqlCom.Parameters.AddWithValue("@userName", entity.Username);
                sqlCom.Parameters.AddWithValue("@age", entity.Age);
                sqlCom.ExecuteNonQuery();
            }
            
        }

        public void Delete(int id)
        {
            using (SqlConnection sqlCon = new SqlConnection(_connectionString))
            {
                sqlCon.Open();
                SqlCommand sqlCom = new SqlCommand();
                sqlCom.Connection = sqlCon;
                sqlCom.CommandText = "DELETE FROM User WHERE Id = @Id";
                sqlCom.Parameters.AddWithValue("@Id", id);
                sqlCom.ExecuteNonQuery();
            }
        }

        public List<User> GetAll()
        {
            using (SqlConnection sqlCon = new SqlConnection(_connectionString))
            {
                sqlCon.Open();
                SqlCommand sqlCom = new SqlCommand();
                sqlCom.Connection = sqlCon;
                sqlCom.CommandText = "SELECT * FROM User";
                List<User> users = new List<User>();
                SqlDataReader reader = sqlCom.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(new User
                    {
                        FirstName = (string)reader["FirstName"],
                        LastName = (string)reader["LastName"],
                        Username = (string)reader["Username"],
                        Age = (int)reader["Age"]
                    }); 
                }
                return users;
            }
        }

        public User GetById(int id)
        {
            using (SqlConnection sqlCon = new SqlConnection(_connectionString))
            {
                sqlCon.Open();
                SqlCommand sqlCom = new SqlCommand();
                sqlCom.Connection = sqlCon;
                sqlCom.CommandText = "SELECT * FROM User WHERE Id = @id";
                sqlCom.Parameters.AddWithValue("@id", id);
                User user = new User();
                SqlDataReader reader = sqlCom.ExecuteReader();
                if (reader.Read())
                {
                    user.FirstName = (string)reader["FirstName"];
                    user.LastName = (string)reader["LastName"];
                    user.Username = (string)reader["Username"];
                    user.Age = (int)reader["Age"];
                }
                return user;
            }
        }

        public void Update(User update)
        {
            using (SqlConnection sqlCon = new SqlConnection(_connectionString))
            {
                sqlCon.Open();
                SqlCommand sqlCom = new SqlCommand();
                sqlCom.Connection = sqlCon;
                sqlCom.CommandText = "UPDATE User SET FirstName = @firstName, LastName = @lastName, Username = @userName, Age = @age WHERE Id = @id";
                sqlCom.Parameters.AddWithValue("@firstName", update.FirstName);
                sqlCom.Parameters.AddWithValue("@lastName", update.LastName);
                sqlCom.Parameters.AddWithValue("@userName", update.Username);
                sqlCom.Parameters.AddWithValue("@age", update.Age);
                sqlCom.Parameters.AddWithValue("@id", update.Id);
                sqlCom.ExecuteNonQuery();
            }
        }
    }
}
