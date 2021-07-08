using System;
using CommonInterfaces;
using DALInterfaces;
using CommonLogic;
using System.Data;
using System.Data.SqlClient;

namespace SqlDAL
{
    class SQLAuthentificator : IAuthentificator
    {
        IDataLayer _DAO;

        public SQLAuthentificator(IDataLayer dataLayer)
        {
            if (dataLayer.GetType() != typeof(UserDAL))
                throw new Exception("Инициализация аутентификатора из неверного класса DAL!");

            _DAO = dataLayer;
        }


        public bool CheckUserIdentity(string userName, string password)
        {
            using (SqlConnection connection = new SqlConnection(Common._connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("CheckIdentity", connection);

                command.Parameters.AddWithValue("@UserId", _DAO.GetEntityId(userName));
                command.Parameters.AddWithValue("@PasswordHashSum",
                    new PasswordHasher().HashThePassword(password));

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
    }
}
