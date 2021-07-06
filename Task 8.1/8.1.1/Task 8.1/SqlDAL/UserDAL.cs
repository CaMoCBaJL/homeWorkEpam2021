using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonInterfaces;
using DALInterfaces;
using Entities;

namespace SqlDAL
{
    public class UserDAL : IDataLayer
    {
        public int EntityCount => Users.Count;

        List<User> Users
        {
            get
            {
                if (Users == null)
                    Users = GetEntitiesFromDB();

                return Users;
            }
            set { }
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

                }
                catch (Exception)
                {
                    return new List<User>();
                }
            }

            var connectedEntities = GetConnectedEntities() as List<Award>;

            foreach (var entity in result)
            {
                foreach (var award in connectedEntities)
                {
                    if (award.ConnectedEntities.Contains(entity.Id))
                        entity.ConnectedEntities.Add(award.Id);
                }
            }

            return result;
        }

        public bool AddEntity(CommonEntity entity, string passwordHashSum)
        {
            if (Users.FindIndex(User => User.Id == entity.Id) != -1)
                return false;
            else
            {
                var user = entity as User;

                using (SqlConnection connection = new SqlConnection(Common._connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("AddAward", connection);

                    SqlParameter connectedIds = new SqlParameter("@ConnectedIds", SqlDbType.Int);
                    connectedIds.Value = entity.ConnectedEntities;

                    command.Parameters.AddWithValue("@UserId", user.Id);
                    command.Parameters.AddWithValue("@UserName", user.Name);
                    command.Parameters.AddWithValue("@UserBirthDate", user.DateOfBirth);
                    command.Parameters.AddWithValue("@UserAge", user.Age);
                    command.Parameters.AddWithValue("@PasswordHashSum", passwordHashSum);

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

            foreach (var award in result)
            {
                foreach (var user in Users)
                {
                    if (user.ConnectedEntities.Contains(award.Id))
                        award.ConnectedEntities.Add(user.Id);
                }
            }

            return result;
        }

        public IEnumerable<CommonEntity> GetEntities() => Users;

        public int GetEntityId(string entityName)
            => Users.FindIndex(User => User.Name == entityName);

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
