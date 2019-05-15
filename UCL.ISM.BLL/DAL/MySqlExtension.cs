using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Text;

namespace UCL.ISM.BLL.DAL
{
    public class MySqlExtension<T> : Database
    {
        
        MySqlConnection conn;
        MySqlCommand cmd;

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

        public List<T> ExecuteReaderList(string query, T source)
        {
            cmd = new MySqlCommand();
            Get_Connection();
            List<T> temp = new List<T>();

            using (cmd.Connection = conn)
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;
                try
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int index = reader.FieldCount;
                            object[] vs = new object[index][];
                            T item = source;
                            for (int i = 0; i < index; i++)
                            {
                                for (int j = 0; j < index; j++)
                                {
                                    vs[i] = new object[]
                                    {
                                        reader.GetFieldValue<T>(j)
                                    };
                                }

                                //temp.Add();
                            }
                        }
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
            return temp;
        }
    }
}
