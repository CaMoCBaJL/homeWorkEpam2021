using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CommonLogic;
using CommonInterfaces;
using DALInterfaces;
using Entities;

namespace SqlDAL
{
    public class UserDAL : IDataLayer
    {
        public int EntityCount => GetEntitiesFromDB().Count;


        public UserDAL()
        {
            if (!AdminExists)
                AddAdmin();
        }

        bool AdminExists { get => GetEntitiesFromDB().FindIndex(user => user.Id == 0) != -1; }

        void AddAdmin()
        {
            AddEntity(new User(0, "admin", "0.0.0", 0, new List<int>()), "admin");
        }

        public List<User> GetEntitiesFromDB()
        {
            List<User> result = new List<User>();

            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("GetUsers", connection);

                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Add(
                             new User(
                                 id: (int)reader["ID"],
                                 name: reader["UserName"] as string,
                                 age: (int)reader["Age"],
                                 dateOfBirth: reader["BirthDate"] as string,
                                 userAwards: new List<int>()
                             ));
                    }

                    foreach (var user in result)
                    {
                        reader.Close();

                        command = new SqlCommand("GetUserAwards", connection);

                        command.Parameters.AddWithValue("@UserId", user.Id);

                        command.CommandType = CommandType.StoredProcedure;

                        reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            user.ConnectedEntities.Add((int)reader["AwardId"]);
                        }
                    }

                }
                catch (Exception)
                {
                    return new List<User>();
                }
            }

            return result;
        }

        public bool AddEntity(CommonEntity entity, string password)
        {
            if (GetEntitiesFromDB().FindIndex(User => User.Id == entity.Id) != -1)
                return false;
            else
            {
                var user = entity as User;

                using (SqlConnection connection = new SqlConnection(Common._connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("AddUser", connection);

                    SqlParameter connectedIds = new SqlParameter("@ConnectedIds", SqlDbType.Int);
                    connectedIds.Value = entity.ConnectedEntities;

                    command.Parameters.AddWithValue("@UserId", user.Id);
                    command.Parameters.AddWithValue("@UserName", user.Name);
                    command.Parameters.AddWithValue("@UserBirthDate", user.DateOfBirth);
                    command.Parameters.AddWithValue("@UserAge", user.Age);
                    command.Parameters.AddWithValue("@PasswordHashSum", new PasswordHasher().HashThePassword(password));

                    var specParam = new SqlParameter("@UserAwards", SqlDbType.Structured);
                    specParam.Value = Common.ParseConnectedIds(user.ConnectedEntities);
                    command.Parameters.Add(specParam);

                    command.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        return false;
                    }

                    return true;
                }
            }
        }

        public IEnumerable<CommonEntity> GetConnectedEntities()
        {
            List<Award> result = new List<Award>();

            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("GetAwards", connection);

                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Add(
                            new Award(
                                id: (int)reader["ID"],
                                title: reader["Title"] as string,
                                connectedEntities: new List<int>()
                            ));
                    }

                }
                catch (Exception)
                {
                    return new List<Award>();
                }

            }

            return result;
        }

        public IEnumerable<CommonEntity> GetEntities() => GetEntitiesFromDB();

        public int GetEntityId(string entityName)
        {
            var userToFind = GetEntitiesFromDB().Find(user => user.Name == entityName);

            if (userToFind != null)
                return userToFind.Id;
            else
                return -1;
        }

        public IAuthentificator CreateAuthentificator() => new SQLAuthentificator(this);

        public bool RemoveEntity(int entityId)
        {
            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("RemoveEntity", connection);
                command.Parameters.AddWithValue("@EntityType", 1);

                command.Parameters.AddWithValue("@EntityId", entityId);

                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return true;
        }

        public bool UpdateEntity(CommonEntity entity)
        {
            var user = entity as User;

            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("UpdateUser", connection);

                SqlParameter connectedIds = new SqlParameter("@ConnectedIds", SqlDbType.Int);
                connectedIds.Value = entity.ConnectedEntities;

                command.Parameters.AddWithValue("@UserId", user.Id);
                command.Parameters.AddWithValue("@UserName", user.Name);
                command.Parameters.AddWithValue("@UserBirthDate", user.DateOfBirth);
                command.Parameters.AddWithValue("@UserAge", user.Age);

                var specParam = new SqlParameter("@UserAwards", SqlDbType.Structured);
                specParam.Value = Common.ParseConnectedIds(user.ConnectedEntities);
                command.Parameters.Add(specParam);

                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    return false;
                }

                return true;
            }
        }
    }
}
