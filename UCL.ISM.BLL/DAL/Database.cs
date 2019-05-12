using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace UCL.ISM.BLL.DAL
{
    public class Database
    {
        private bool connection_open;
        public MySqlConnection connection;

        public void Get_Connection()
        {
            connection = new MySqlConnection();

            connection.ConnectionString = "Server=mysql72.unoeuro.com; user id=pineappledevelopers_com;password=Bregnevej942; Allow User Variables=True;persist security info=true;database=pineappledevelopers_com_db2";

            if (Open_Local_Connection())
            {
                connection_open = true;
            }
            else
            {

            }

        }
        public bool Open_Local_Connection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void CloseConnection()
        {
            connection.Close();
        }
    }
}
