namespace SocialApp.Repository
{
    using System.Collections.Generic;
    using Microsoft.Data.SqlClient;
    using AppCommonClasses.Models;
    using AppCommonClasses.Interfaces;

    public class UserRepository : IUserRepository
    {
        private const string ConnectionString = "Data Source=vm;" +
            "Initial Catalog=team_babes;" +
            "Integrated Security=True;Encrypt=False;TrustServerCertificate=True"; 
        private readonly SqlConnection connection;


        public UserRepository() : this(ConnectionString) { }

        public UserRepository(string loginString)
        {
            this.connection = new SqlConnection(loginString);
        }

        public List<User> GetAll()
        {
            this.connection.Open();
            var users = new List<User>();

            using (var command = new SqlCommand("SELECT * FROM Users", this.connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    users.Add(this.CreateUserFromReader(reader));
                }
            }

            this.connection.Close();
            return users;
        }

        public List<User> GetUserFollowers(long userId)
        {
            this.connection.Open();
            var followers = new List<User>();

            using (var command = new SqlCommand(
                "SELECT * FROM Users WHERE Id IN (SELECT FollowerId FROM UserFollowers WHERE UserId = @UserId)",
                this.connection))
            {
                command.Parameters.AddWithValue("@UserId", userId);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        followers.Add(this.CreateUserFromReader(reader));
                    }
                }
            }

            this.connection.Close();
            return followers;
        }

        public List<User> GetUserFollowing(long userId)
        {
            this.connection.Open();
            var following = new List<User>();

            using (var command = new SqlCommand(
                "SELECT * FROM Users WHERE Id IN (SELECT UserId FROM UserFollowers WHERE FollowerId = @UserId)",
                this.connection))
            {
                command.Parameters.AddWithValue("@UserId", userId);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        following.Add(this.CreateUserFromReader(reader));
                    }
                }
            }

            this.connection.Close();
            return following;
        }

        public void Follow(long followerId, long followeeId)
        {
            this.connection.Open();
            using (var command = new SqlCommand(
                "INSERT INTO UserFollowers (UserId, FollowerId) VALUES (@UserId, @FollowerId)",
                this.connection))
            {
                command.Parameters.AddWithValue("@UserId", followeeId);
                command.Parameters.AddWithValue("@FollowerId", followerId);
                command.ExecuteNonQuery();
            }
            this.connection.Close();
        }

        public void Unfollow(long followerId, long followeeId)
        {
            this.connection.Open();
            using (var command = new SqlCommand(
                "DELETE FROM UserFollowers WHERE UserId = @UserId AND FollowerId = @FollowerId",
                this.connection))
            {
                command.Parameters.AddWithValue("@UserId", followeeId);
                command.Parameters.AddWithValue("@FollowerId", followerId);
                command.ExecuteNonQuery();
            }

            this.connection.Close();
        }

        public User GetByEmail(string email)
        {
            this.connection.Open();
            using (var command = new SqlCommand("SELECT * FROM Users WHERE Email = @Email", this.connection))
            {
                command.Parameters.AddWithValue("@Email", email);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return this.CreateUserFromReader(reader);
                    }
                }
            }

            this.connection.Close();
            return null;
        }

        public User GetById(long userId)
        {
            this.connection.Open();
            using (var command = new SqlCommand("SELECT * FROM Users WHERE Id = @UserId", this.connection))
            {
                command.Parameters.AddWithValue("@UserId", userId);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return this.CreateUserFromReader(reader);
                    }
                }
            }

            this.connection.Close();

            return null;
        }

        public void Save(User user)
        {
            this.connection.Open();
            using (var command = new SqlCommand(
                "INSERT INTO Users (Username, Email, PasswordHash, Image) VALUES (@Username, @Email, @PasswordHash, @Image)",
                this.connection))
            {
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                command.Parameters.AddWithValue("@Image", user.Image);
                command.ExecuteNonQuery();
            }

            this.connection.Close();
        }

        public void UpdateById(long userId, string username, string email, string passwordHash, string image)
        {
            this.connection.Open();
            using (var command = new SqlCommand(
                "UPDATE Users SET Username = @Username, Email = @Email, PasswordHash = @PasswordHash, Image = @Image WHERE Id = @UserId",
                this.connection))
            {
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@PasswordHash", passwordHash);
                command.Parameters.AddWithValue("@Image", image);
                command.ExecuteNonQuery();
            }

            this.connection.Close();
        }

        public void DeleteById(long userId)
        {
            this.connection.Open();
            using (var command = new SqlCommand("DELETE FROM Users WHERE Id = @UserId", this.connection))
            {
                command.Parameters.AddWithValue("@UserId", userId);
                command.ExecuteNonQuery();
            }

            this.connection.Close();
        }

        private User CreateUserFromReader(SqlDataReader reader)
        {
            return new User
            {
                Id = reader.GetInt64(reader.GetOrdinal("Id")),
                Username = reader.GetString(reader.GetOrdinal("Username")),
                Email = reader.GetString(reader.GetOrdinal("Email")),
                PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash")),
                Image = reader.IsDBNull(reader.GetOrdinal("Image")) ? string.Empty : reader.GetString(reader.GetOrdinal("Image"))
            };
        }
    }
}