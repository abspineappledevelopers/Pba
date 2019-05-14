using System;
using System.Collections.Generic;
using System.Text;
using UCL.ISM.BLL.Interface;
using UCL.ISM.BLL.BLL;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace UCL.ISM.BLL.DAL
{
    public class InterviewSchemeDB
    {
        private Database _db;
        private MySqlCommand cmd;

        public InterviewSchemeDB()
        {
            _db = new Database();
            cmd = new MySqlCommand();
        }

        public int CreateNewInterviewScheme(IInterviewScheme interview)
        {
            string query = "INSERT INTO UCL_InterviewScheme(Comment) VALUES (@Comment)";
            string query2 = "INSERT INTO UCL_InterviewSchemeForCountry(Country, InterviewScheme) VALUES (@CountryId, @InterviewScheme)";
            string param1 = "@Comment";
            string param2 = "@CountryId";
            string param3 = "@InterviewScheme";
            
            return ExecuteTrans(query, query2, SetParameterWithValue(param1, interview.Comment), param2, param3, interview);
        }

        public void AddQuestion(IQuestion question)
        {
            string query = "INSERT INTO UCL_Question(Id, Question, InterviewScheme) VALUES (@Id, @Question, @InterviewScheme)";
            string param1 = "@Id";
            string param2 = "@Question";
            string param3 = "@InterviewScheme";

            List<MySqlParameter> temp = new List<MySqlParameter>();
            temp.Add(SetParameterWithValue(param1, Guid.NewGuid()));
            temp.Add(SetParameterWithValue(param2, question.Quest));
            temp.Add(SetParameterWithValue(param3, question.InterviewSchemeId));

            ExecuteCmd(query, temp);
        }
        

        public void RemoveQuestion(IQuestion question)
        {
            throw new NotImplementedException();
        }

        public IInterviewScheme GetInterviewScheme(int id)
        {
            string query = "SELECT * FROM UCL_INTERVIEWSCHEME WHERE Id = @Id";

            return ExecuteReaderScheme(query);
        }

        public List<IQuestion> GetAllSchemeQuestions(int id)
        {
            string query = "SELECT * FROM UCL_Question WHERE InterviewScheme =" + id;

            return ExecuteReaderQuestions(query);
        }

        public void UpdateInterviewScheme(int id)
        {
            string param1 = "@Id";
            string query = "UPDATE UCL_INTERVIEWSCHEME WHERE Id = @Id";

            ExecuteCmd(query, SetParameterWithValue(param1, id));
        }

        public void DeleteInterviewScheme(int id)
        {
            string param1 = "@Id";
            string query = "DELETE FROM UCL_INTERVIEWSCHEME WHERE Id = @id";

            ExecuteCmd(query, SetParameterWithValue(param1, id));
        }

        #region Functionality
        private List<IQuestion> ExecuteReaderQuestions(string query)
        {
            _db.Get_Connection();
            List<IQuestion> temp = new List<IQuestion>();

            using (cmd.Connection = _db.connection)
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;

                try
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        IQuestion quest;

                        while (reader.Read())
                        {
                            quest = new Question();
                            quest.Id = reader.GetGuid(0);
                            quest.Quest = reader.GetString(1).ToString();
                            quest.InterviewSchemeId = reader.GetInt32(3);
                            temp.Add(quest);
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
            return temp;
        }

        private IInterviewScheme ExecuteReaderScheme(string query)
        {
            _db.Get_Connection();
            IInterviewScheme scheme = new InterviewScheme();

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
                            scheme.Id = reader.GetInt32(0);
                            scheme.CreatedDate = reader.GetDateTime(1);
                            scheme.EditedDate = reader.GetDateTime(2);
                            scheme.Comment = reader.GetString(3);
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
                return scheme;
            }
        }

        private int ExecuteTrans(string query, string query2, MySqlParameter param, string param2, string param3, object value)
        {
            var schemeId = 0;
            _db.Get_Connection();

            // Must assign both transaction object and connection
            // to Command object for a pending local transaction
            cmd.Connection = _db.connection;
            cmd.Transaction = _db.connection.BeginTransaction();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = query;
            cmd.Parameters.Add(param);
            try
            {
                cmd.ExecuteNonQuery();
                schemeId = Convert.ToInt32(cmd.LastInsertedId);
                var type = value as InterviewScheme;
                for (int i = 0; i < type.CountryId.Count; i++)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = query2;
                    cmd.Parameters.Add(SetParameterWithValue(param2, type.CountryId[i]));
                    cmd.Parameters.Add(SetParameterWithValue(param3, schemeId));
                    cmd.ExecuteNonQuery();
                }
                cmd.Transaction.Commit();
            }
            catch (Exception)
            {
                try
                {
                    cmd.Transaction.Rollback();
                }
                catch (SqlException ex)
                {
                    if (cmd.Transaction.Connection != null)
                    {

                    }
                }
            }
            finally
            {
                cmd.Connection.Close();
            }
            return schemeId;
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
