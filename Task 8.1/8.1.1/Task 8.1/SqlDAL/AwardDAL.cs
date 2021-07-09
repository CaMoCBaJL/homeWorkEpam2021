using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CommonInterfaces;
using DALInterfaces;
using Entities;

namespace SqlDAL
{
    public class AwardDAL : IDataLayer
    {
        public int EntityCount => GetEntitiesFromDB().Count;


        public bool AddEntity(CommonEntity entity, string passwordHashSum)
        {
            if (GetEntitiesFromDB().FindIndex(award => award.Id == entity.Id) != -1)
                return false;
            else
            {
                var award = entity as Award;

                using (SqlConnection connection = new SqlConnection(Common._connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("AddAward", connection);

                    command.Parameters.AddWithValue("@AwardId", award.Id);
                    command.Parameters.AddWithValue("@AwardTitle", award.Title);

                    var lastParam = new SqlParameter("@AwardedUsers", SqlDbType.Structured);
                    lastParam.Value = Common.ParseConnectedIds(award.ConnectedEntities);
                    command.Parameters.Add(lastParam);

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
                    return new List<Award>();
                }

            }

            foreach (var user in result)
            {
                foreach (var award in GetEntitiesFromDB())
                {
                    if (award.ConnectedEntities.Contains(user.Id))
                        user.ConnectedEntities.Add(award.Id);
                }
            }

            return result;
        }

        public List<Award> GetEntitiesFromDB()
        {
            List<Award> result = new List<Award>();

            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("GetGetEntitiesFromDB()", connection);

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

                    foreach (var award in result)
                    {
                        reader.Close();

                        command = new SqlCommand("GetAwardedusers", connection);

                        command.Parameters.AddWithValue("@AwardId", award.Id);

                        command.CommandType = CommandType.StoredProcedure;

                        reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            award.ConnectedEntities.Add((int)reader["UserId"]);
                        }
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
            var awardToFind = GetEntitiesFromDB().Find(award => award.Title == entityName);

            if (awardToFind != null)
                return awardToFind.Id;
            else
                return -1;
        }


        public bool RemoveEntity(int entityId)
        {
            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("RemoveEntity", connection);
                command.Parameters.AddWithValue("@EntityType", 0);

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
            var award = entity as Award;

            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("UpdateAward", connection);

                SqlParameter connectedIds = new SqlParameter("@ConnectedIds", SqlDbType.Int);
                connectedIds.Value = entity.ConnectedEntities;

                command.Parameters.AddWithValue("@AwardId", award.Id);
                command.Parameters.AddWithValue("@AwardTitle", award.Title);

                var lastParam = new SqlParameter("@AwardedUsers", SqlDbType.Structured);
                lastParam.Value = Common.ParseConnectedIds(award.ConnectedEntities);
                command.Parameters.Add(lastParam);

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

        public IAuthentificator CreateAuthentificator() => new SQLAuthentificator(this);
    }
}
