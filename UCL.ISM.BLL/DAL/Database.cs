using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace UCL.ISM.BLL.DAL
{
    public class Database
    {
        public MySqlConnection conn;
        private MySqlCommand cmd;

        public Database()
        {
            cmd = new MySqlCommand();
        }

        public void Get_Connection()
        {
            conn = new MySqlConnection();

            conn.ConnectionString = "Server=mysql72.unoeuro.com; user id=pineappledevelopers_com;password=Bregnevej942; Allow User Variables=True;persist security info=true;database=pineappledevelopers_com_db2";
            
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }
            else if (conn.State == System.Data.ConnectionState.Broken)
            {
                conn.Close();
                conn.Open();
            }
        }

        public void CloseConnection()
        {
            conn.Close();
        }

        public List<object> ExecuteReader(string query, object type, object prop1, object prop2, object prop3)
        {
            List<object> list = new List<object>();
            
            Get_Connection();

            using (cmd.Connection = conn)
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        string input = type.GetType().ToString();
                        object v = new object();
                        //IInterviewer iv = new Interviewer();
                        //iv.Id = reader.GetString(0).ToString();
                        //iv.Firstname = reader.GetString(1).ToString();
                        //iv.Lastname = reader.GetString(2).ToString();

                        list.Add(v);
                    }
                }
            }
            return list;
        }

        public void ExecuteCmd(string query, List<MySqlParameter> parameters)
        {
            Get_Connection();

            using (cmd.Connection = conn)
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;

                foreach (var item in parameters)
                {
                    cmd.Parameters.Add(item);
                }

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }
                }
            }
        }

        public void ExecuteCmd(string query, MySqlParameter param1, MySqlParameter param2)
        {
            Get_Connection();

            using (cmd.Connection = conn)
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;
                cmd.Parameters.Add(param1);
                cmd.Parameters.Add(param2);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }
                }

            }
        }

        public void ExecuteCmd(string query, MySqlParameter param)
        {
            Get_Connection();

            using (cmd.Connection = conn)
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;
                cmd.Parameters.Add(param);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }
                }

            }
        }

        private MySqlParameter SetParameter(string param, MySqlDbType type, int size)
        {
            return new MySqlParameter(param, type, size);
        }
        private MySqlParameter SetParameter(string param, MySqlDbType type)
        {
            return new MySqlParameter(param, type);
        }

        public MySqlParameter SetParameterWithValue(string param, object value)
        {
            return new MySqlParameter(param, value);
        }
    }
}
