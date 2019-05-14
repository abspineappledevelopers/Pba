using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using UCL.ISM.BLL.DAL;
using UCL.ISM.BLL.Interface;

namespace UCL.ISM.BLL
{
    class StudyFieldDB
    {
        private Database _db;
        private MySqlCommand cmd;
        StudyField _sf;
        List<IStudyField> _listsf;

        public StudyFieldDB()
        {
            _db = new Database();
            cmd = new MySqlCommand();
        }

        public void CreateNewStudyField(string fieldName)
        {
            string query = "INSERT INTO UCL_StudyField(Name) VALUES (@Name)";
            string param1 = "@Name";
            
            ExecuteCmd(query, SetParameterWithValue(param1, fieldName));
        }

        public IStudyField GetStudyField(int Id)
        {
            string query = "SELECT * FROM UCL_StudyField WHERE Id = @Id";
            throw new NotImplementedException();
        }

        public List<IStudyField> GetAllStudyFields()
        {
            string query = "SELECT * FROM UCL_StudyField";

            return ExecuteReaderStudyFields(query);
        }

        public void EditStudyField(IStudyField studyField)
        {
            string query = "UPDATE UCL_StudyField SET Name = @Name WHERE Id = @id;";
            string param1 = "@id";
            string param2 = "@Name";
            ExecuteCmd(query, SetParameterWithValue(param1, studyField.Id), SetParameterWithValue(param2, studyField.FieldName));
        }

        public void DeleteStudyField(int Id)
        {
            string query = "DELETE FROM UCL_StudyField WHERE Id = @id;";
            string param1 = "@id";

            ExecuteCmd(query, SetParameterWithValue(param1, Id));
        }

        #region Functionality
        private List<IStudyField> ExecuteReaderStudyFields(string query)
        {
            _db.Get_Connection();
            List<IStudyField> list = new List<IStudyField>();

            using (cmd.Connection = _db.connection)
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;

                try
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            IStudyField sf = new StudyField();
                            sf.Id = Convert.ToInt32(reader.GetInt32(0));
                            sf.FieldName = reader.GetString(1).ToString();
                            sf.Created = reader.GetDateTime(2);
                            sf.Edited = reader.GetDateTime(3);

                            list.Add(sf);
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    if (_db.connection.State == System.Data.ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }
                }
            }
            return list;
        }

        private void ExecuteCmd(string query, List<MySqlParameter> parameters)
        {
            _db.Get_Connection();

            using (cmd.Connection = _db.connection)
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
                    if (_db.connection.State == System.Data.ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }
                }
            }
        }

        private void ExecuteCmd(string query, MySqlParameter param1, MySqlParameter param2)
        {
            _db.Get_Connection();

            using (cmd.Connection = _db.connection)
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
                    if (_db.connection.State == System.Data.ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }
                }

            }
        }

        private void ExecuteCmd(string query, MySqlParameter param)
        {
            _db.Get_Connection();

            using (cmd.Connection = _db.connection)
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
                    if (_db.connection.State == System.Data.ConnectionState.Open)
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

        private MySqlParameter SetParameterWithValue(string param, object value)
        {
            return new MySqlParameter(param, value);
        }
        #endregion
    }
}
