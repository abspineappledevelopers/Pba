using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace UCL.ISM.BLL.DAL
{
    public class MySqlExtension<T>
    {
        
        protected MySqlConnection conn;
        protected MySqlCommand cmd;

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

        public T ExecuteReader(string query, T source)
        {
            cmd = new MySqlCommand();
            Get_Connection();

            using (cmd.Connection = conn)
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;
                T item;
                try
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                item = source;

                                reader.GetFieldValue<T>(i);
                            }
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                return item = source;
            }
        }

        public object[] ExecuteReaderList(string query, object source)
        {
            object[] vs;
            cmd = new MySqlCommand();
            Get_Connection();

            using (cmd.Connection = conn)
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;
                cmd.Parameters.Add(SetParameterWithValue("@Id", source));
                try
                {
                    int rows = 100;
                    vs = new object[rows];
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int j = 0;
                        int index = reader.FieldCount;
                        while (reader.Read())
                        {
                            object[] temp = new object[index];
                            for (int i = 0; i < index; i++)
                            {
                                temp[i] = reader[i];
                                
                            }
                            vs[j] = temp;
                            j++;
                        }
                        return vs;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    cmd.Connection.Close();
                }
            }
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
            cmd = new MySqlCommand();
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
            cmd = new MySqlCommand();
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
            cmd = new MySqlCommand();
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
        public List<MySqlParameter> SetParametersList(List<string> param, List<object> value)
        {
            List<MySqlParameter> temp = new List<MySqlParameter>();

            for (int i = 0; i < param.Count; i++)
            {
                temp.Add(SetParameterWithValue(param[i], value[i]));
            }

            return temp;
        }
    }
}
