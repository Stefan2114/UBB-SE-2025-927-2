// <copyright file="GroupRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SocialApp.Repository
{
    using AppCommonClasses.Interfaces;
    using AppCommonClasses.Models;
    using Microsoft.Data.SqlClient;
    using System;
    using System.Collections.Generic;
    using Group = AppCommonClasses.Models.Group;


    /// <summary>
    /// Repository class for managing Group entities.
    /// </summary>
    public partial class GroupRepository : IGroupRepository
    {
        private string loginString = "Server=DESKTOP-5A6VJDA;Database=SocialApp;Trusted_Connection=True;TrustServerCertificate=True;";

        private SqlConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupRepository"/> class.
        /// </summary>
        public GroupRepository()
        {
            this.connection = new SqlConnection(this.loginString);
        }

        /// <summary>
        /// Gets all groups from the database.
        /// </summary>
        /// <returns>
        /// a list of all groups.
        /// </returns>
        public List<Group> GetAllGroups()
        {
            List<Group> ans = new List<Group>();
            this.connection.Open();

            SqlCommand selectCommand = new SqlCommand("SELECT * FROM Groups", this.connection);
            SqlDataReader reader = selectCommand.ExecuteReader();
            while (reader.Read())
            {
                Group group = new Group
                {
                    Id = reader.GetInt64(reader.GetOrdinal("Id")),
                    Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? string.Empty : reader.GetString(reader.GetOrdinal("Name")),
                    AdminId = reader.GetInt64(reader.GetOrdinal("AdminId")),
                    Image = reader.IsDBNull(reader.GetOrdinal("Image")) ? string.Empty : reader.GetString(reader.GetOrdinal("Image")),
                    Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? string.Empty : reader.GetString(reader.GetOrdinal("Description")),
                };

                ans.Add(group);
            }

            reader.Close();
            this.connection.Close();

            return ans;
        }

        /// <summary>
        /// Gets all users from a specific group.
        /// </summary>
        /// <param name="id">
        /// the id of the group.
        /// </param>
        /// <returns>
        /// a list of users in the group.
        /// </returns>
        public List<User> GetUsersFromGroup(long id)
        {
            this.connection.Open();
            List<User> ans = new List<User>();
            SqlCommand selectCommand = new SqlCommand(
                "SELECT * FROM Users WHERE Id IN (SELECT UserId FROM GroupUsers WHERE GroupId = @Id)",
                this.connection);
            selectCommand.Parameters.AddWithValue("@Id", id);

            SqlDataReader reader = selectCommand.ExecuteReader();
            while (reader.Read())
            {
                User user = new User
                {
                    Id = reader.GetInt64(reader.GetOrdinal("Id")),
                    Username = reader.GetString(reader.GetOrdinal("Username")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash")),
                    Image = reader.IsDBNull(reader.GetOrdinal("Image")) ? string.Empty : reader.GetString(reader.GetOrdinal("Image")),
                };
                ans.Add(user);
            }

            reader.Close();
            this.connection.Close();
            return ans;
        }

        /// <summary>
        /// Gets all groups for a specific user.
        /// </summary>
        /// <param name="userId">
        /// the id of the user.
        /// </param>
        /// <returns>
        /// a list of groups the user is part of.
        /// </returns>
        public List<Group> GetGroupsForUser(long userId)
        {
            this.connection.Open();
            List<Group> ans = new List<Group>();
            SqlCommand selectCommand = new SqlCommand(
                "SELECT * FROM Groups WHERE Id IN (SELECT GroupId FROM GroupUsers WHERE UserId = @UserId)",
                this.connection);
            selectCommand.Parameters.AddWithValue("@UserId", userId);
            SqlDataReader reader = selectCommand.ExecuteReader();

            while (reader.Read())
            {
                Group group = new Group
                {
                    Id = reader.GetInt64(reader.GetOrdinal("Id")),
                    Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? string.Empty : reader.GetString(reader.GetOrdinal("Name")),
                    AdminId = reader.GetInt64(reader.GetOrdinal("AdminId")),
                    Image = reader.IsDBNull(reader.GetOrdinal("Image")) ? string.Empty : reader.GetString(reader.GetOrdinal("Image")),
                    Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? string.Empty : reader.GetString(reader.GetOrdinal("Description")),
                };
                ans.Add(group);
            }

            reader.Close();
            this.connection.Close();
            return ans;
        }

        /// <summary>
        /// Deletes a group by its ID.
        /// </summary>
        /// <param name="id">
        /// the ID of the group to delete.
        /// </param>
        public void DeleteGroupById(long id)
        {
            this.connection.Open();

            SqlCommand deleteCommand = new SqlCommand("DELETE FROM Groups WHERE Id = @Id", this.connection);
            string queryParam = id.ToString();
            deleteCommand.Parameters.AddWithValue("@Id", queryParam);
            deleteCommand.ExecuteNonQuery();

            this.connection.Close();
        }

        /// <summary>
        /// Gets a group by its ID.
        /// </summary>
        /// <param name="id">The ID of the group to retrieve.</param>
        /// <returns>The group with the specified ID, or null if not found.</returns>
        public Group GetGroupById(long id)
        {
            this.connection.Open();
            Group? group = null;

            SqlCommand selectCommand = new SqlCommand("SELECT * FROM Groups WHERE Id = @Id", this.connection);
            selectCommand.Parameters.AddWithValue("@Id", id);

            SqlDataReader reader = selectCommand.ExecuteReader();
            if (reader.Read())
            {
                group = new Group
                {
                    Id = reader.GetInt64(reader.GetOrdinal("Id")),
                    Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? string.Empty : reader.GetString(reader.GetOrdinal("Name")),
                    AdminId = reader.GetInt64(reader.GetOrdinal("AdminId")),
                    Image = reader.IsDBNull(reader.GetOrdinal("Image")) ? string.Empty : reader.GetString(reader.GetOrdinal("Image")),
                    Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? string.Empty : reader.GetString(reader.GetOrdinal("Description")),
                };
            }

            reader.Close();
            this.connection.Close();
            return group!;
        }

        /// <summary>
        /// Saves a new group to the database.
        /// </summary>
        /// <param name="entity">
        /// the group entity to save.
        /// </param>
        public void SaveGroup(Group entity)
        {
            this.connection.Open();

            SqlCommand insertCommand = new SqlCommand(
                "INSERT INTO Groups (Name, AdminId, Image, Description) VALUES (@Name, @AdminId, @Image, @Description); " +
                "SELECT SCOPE_IDENTITY();",
                this.connection);
            insertCommand.Parameters.AddWithValue("@Name", entity.Name);
            insertCommand.Parameters.AddWithValue("@AdminId", entity.AdminId);
            insertCommand.Parameters.AddWithValue("@Image", entity.Image);
            insertCommand.Parameters.AddWithValue("@Description", entity.Description);
            entity.Id = Convert.ToInt64(insertCommand.ExecuteScalar());

            insertCommand = new SqlCommand(
                "INSERT INTO GroupUsers (GroupId, UserId) VALUES (@GroupId, @UserId)",
                this.connection);
            insertCommand.Parameters.AddWithValue("@GroupId", entity.Id);
            insertCommand.Parameters.AddWithValue("@UserId", entity.AdminId);
            insertCommand.ExecuteNonQuery();

            this.connection.Close();
        }

        /// <summary>
        /// Updates a group by its ID.
        /// </summary>
        /// <param name="id">
        /// the ID of the group to update.
        /// </param>
        /// <param name="name">
        /// the new name of the group.
        /// </param>
        /// <param name="image">
        /// the new image of the group.
        /// </param>
        /// <param name="description">
        /// the new description of the group.
        /// </param>
        /// <param name="adminId">
        /// the ID of the admin.
        /// </param>
        public void UpdateGroup(long id, string name, string image, string description, long adminId)
        {
            this.connection.Open();

            SqlCommand updateCommand = new SqlCommand(
                "UPDATE Groups SET Name = @Name, AdminId = @AdminId, Description=@Description, Image=@Image WHERE Id = @Id",
                this.connection);

            updateCommand.Parameters.AddWithValue("@Id", id);
            updateCommand.Parameters.AddWithValue("@Name", name);
            updateCommand.Parameters.AddWithValue("@AdminId", adminId);
            updateCommand.Parameters.AddWithValue("@Description", description);
            updateCommand.Parameters.AddWithValue("@Image", image);
            updateCommand.ExecuteNonQuery();

            this.connection.Close();
        }

        /// <summary>
        /// Disposes the resources used by the GroupRepository.
        /// </summary>
        /// <exception cref="NotImplementedException">
        /// Thrown because the method is not implemented.
        /// </exception>
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
