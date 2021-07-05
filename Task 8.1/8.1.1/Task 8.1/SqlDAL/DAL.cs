using Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using DALInterfaces;
using CommonConstants;

namespace SqlDAL
{
    public class DAL : IDataLayer
    {
        internal static string _connectionString = "Data Source=DESKTOP-VHEEB1U;Initial Catalog=DAL;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        List<User> Users { get; set; }

        List<Award> Awards { get; set; }

        public int UsersCount { get => Users.Count; }

        public int AwardsCount { get => Awards.Count; }


        public DAL()
        {
            UpdateData();

            if (!AdminExists())
                AddAdmin();
        }

        void UpdateData()
        {
            Users = GetEntitiesFromDB(EntityType.User).Cast<User>().ToList();

            Awards = GetEntitiesFromDB(EntityType.Award).Cast<Award>().ToList();
        }

        private bool AdminExists() => Users.FindIndex(user => user.Id == 0) != -1;

        private void AddAdmin()
        {
            var admin = new User("admin", "0.0.0", 0, new List<int>(), 0);

            AddEntity(admin, Identity.HashThePassword("admin"));
        }

        public bool DeleteEntity(CommonEntity entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("RemoveEntity", connection);
                switch (entity)
                {
                    case User user:

                        command.Parameters.AddWithValue("@EntityType", 1);
                        break;
                    case Award award:
                        command.Parameters.AddWithValue("@EntityType", 0);
                        break;
                    default:
                        return false;
                }

                command.Parameters.AddWithValue("@EntityId", entity.Id);

                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    return false;
                }

            }

            return true;
        }

        public IEnumerable<CommonEntity> GetEntities(EntityType entityType)
        {
            switch (entityType)
            {
                case EntityType.User:
                    return new List<User>(Users);
                case EntityType.Award:
                    return new List<Award>(Awards);
                case EntityType.None:
                default:
                    return new List<CommonEntity>();
            }
        }

        IEnumerable<CommonEntity> GetEntitiesFromDB(EntityType entityType)
        {
            IEnumerable<CommonEntity> result;

            EntityType additionalEntityType;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command;

                switch (entityType)
                {
                    case EntityType.User:
                        result = new List<User>();

                        command = new SqlCommand("GetUsers", connection);

                        additionalEntityType = EntityType.Award;
                        break;
                    case EntityType.Award:
                        result = new List<Award>();

                        command = new SqlCommand("GetAwards", connection);

                        additionalEntityType = EntityType.User;
                        break;
                    case EntityType.None:
                    default:
                        return new List<CommonEntity>();
                }

                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    result = ParseDataFromQuerry(entityType, command);
                }
                catch (Exception)
                {
                    return new List<CommonEntity>();
                }

            }

            foreach (var entity in result)
            {
                entity.ConnectedEntities.AddRange(GetConnectedEntities(additionalEntityType, entity.Id));
            }

            return result;
        }

        IEnumerable<CommonEntity> ParseDataFromQuerry(EntityType entityType, SqlCommand command)
        {
            IEnumerable<CommonEntity> result;

            switch (entityType)
            {
                case EntityType.User:
                    result = new List<User>();
                    break;
                case EntityType.Award:
                    result = new List<Award>();
                    break;
                case EntityType.None:
                default:
                    return new List<CommonEntity>();
            }

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var b = reader[0];

                var c = reader["ID"];
                switch (entityType)
                {
                    case EntityType.User:
                        result = result.Append(
                             new User(
                                 id: (int)reader["ID"],
                                 name: reader["UserName"] as string,
                                 age: (int)reader["Age"],
                                 dateOfBirth: reader["BirthDate"] as string,
                                 userAwards: new List<int>()
                             ));
                        break;

                    case EntityType.Award:
                        result = result.Append(
                            new Award(
                                id: (int)reader["ID"],
                                title: reader["Title"] as string,
                                awardedUsers: new List<int>()
                            ));
                        break;

                    case EntityType.None:
                    default:
                        break;
                }
            }

            return result;
        }

        List<int> GetConnectedEntities(EntityType entityType, int entityId)
        {
            List<int> result = new List<int>();

            string paramName;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command;

                switch (entityType)
                {
                    case EntityType.User:
                        command = new SqlCommand("GetAwardedUsers", connection);

                        command.Parameters.AddWithValue("@AwardId", entityId);

                        paramName = "UserId";
                        break;
                    case EntityType.Award:
                        command = new SqlCommand("GetUserAwards", connection);

                        command.Parameters.AddWithValue("@UserId", entityId);

                        paramName = "AwardId";
                        break;
                    case EntityType.None:
                    default:
                        return new List<int>();
                }

                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Add((int)reader[paramName]);
                    }
                }
                catch (Exception)
                {
                    return new List<int>();
                }
            }

            return result;
        }

        public int GetEntityId(EntityType entityType, string entityName)
        {
            int indx;

            switch (entityType)
            {
                case EntityType.User:
                    indx = Users.FindIndex(user => user.Name == entityName);
                    if (indx > 0)
                        return Users[indx].Id;
                    else
                        return -1;
                case EntityType.Award:
                    indx = Awards.FindIndex(user => user.Title == entityName);
                    if (indx > 0)
                        return Awards[indx].Id;
                    else
                        return -1;
                case EntityType.None:
                default:
                    return -1;
            }
        }

        public bool AddEntity(CommonEntity entity, string passwordHashSum = "")
             => ExecuteTheOperation(entity, passwordHashSum, DataOperationType.Add);

        public bool UpdateEntity(CommonEntity entity, string passwordHashSum = "")
            => ExecuteTheOperation(entity, passwordHashSum, DataOperationType.Update);

        public bool ExecuteTheOperation(CommonEntity entity, string passwordHashSum, DataOperationType operationType)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(ParseStoredProcedureName(operationType, entity), connection);

                SqlParameter connectedIds = new SqlParameter("@ConnectedIds", SqlDbType.Int);
                connectedIds.Value = entity.ConnectedEntities;

                switch (entity)
                {
                    case User user:
                        command.Parameters.AddWithValue("@UserId", user.Id);
                        command.Parameters.AddWithValue("@UserName", user.Name);
                        command.Parameters.AddWithValue("@UserBirthDate", user.DateOfBirth);
                        command.Parameters.AddWithValue("@UserAge", user.Age);

                        var specParam = new SqlParameter("@UserAwards", SqlDbType.Structured);
                        specParam.Value = ParseConnectedIds(user.ConnectedEntities);
                        command.Parameters.Add(specParam);

                        if (operationType == DataOperationType.Add)
                        {
                            command.Parameters.AddWithValue("@PasswordHashSum", passwordHashSum);
                        }
                        break;
                    case Award award:
                        command.Parameters.AddWithValue("@AwardId", award.Id);
                        command.Parameters.AddWithValue("@AwardTitle", award.Title);

                        var lastParam = new SqlParameter("@AwardedUsers", SqlDbType.Structured);
                        lastParam.Value = ParseConnectedIds(award.ConnectedEntities);
                        command.Parameters.Add(lastParam);
                        break;
                    default:
                        return false;
                }

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

            UpdateData();

            return true;
        }

        DataTable ParseConnectedIds(List<int> connectedIds)
        {
            var result = new DataTable();

            DataColumn idColumn = new DataColumn("Id", typeof(int));

            result.Columns.Add(idColumn);

            for (int i = 0; i < connectedIds.Count; i++)
            {
                DataRow row = result.NewRow();

                row.ItemArray = new object[] { connectedIds[i] };

                result.Rows.Add(row);
            }

            return result;
        }

        public bool CheckUserIdentity(string userName, string password)
        {
            using (SqlConnection connection = new SqlConnection(DAL._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("CheckIdentity", connection);

                command.Parameters.AddWithValue("@UserId", new DAL().GetEntityId(EntityType.User, userName));
                command.Parameters.AddWithValue("@PasswordHashSum", Identity.HashThePassword(password));

                SqlParameter returnValue = new SqlParameter("@value", SqlDbType.Int);
                returnValue.Direction = ParameterDirection.ReturnValue;

                command.Parameters.Add(returnValue);
                command.CommandType = CommandType.StoredProcedure;
                try
                {
                    command.ExecuteReader();

                    if ((int)returnValue.Value == 1)
                        return true;
                    else
                        return false;
                }
                catch (Exception)
                {
                    return false;
                }

            }
        }

        public string ParseStoredProcedureName(DataOperationType operationType, CommonEntity entity)
        {
            switch (entity)
            {
                case User user:
                    switch (operationType)
                    {
                        case DataOperationType.Add:
                            return "AddUser";
                        case DataOperationType.Update:
                            return "UpdateUser";
                        case DataOperationType.None:
                        default:
                            return string.Empty;
                    }

                case Award award:
                    switch (operationType)
                    {
                        case DataOperationType.Add:
                            return "AddAward";
                        case DataOperationType.Update:
                            return "UpdateAward";
                        case DataOperationType.None:
                        default:
                            return string.Empty;
                    }
                default:
                    return string.Empty;
            }
        }
    }
}
