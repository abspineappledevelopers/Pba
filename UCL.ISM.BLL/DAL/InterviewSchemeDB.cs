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
        private readonly string _connectionString;
        private IInterviewScheme _scheme;
        private Database _db;
        private MySqlCommand cmd;

        public InterviewSchemeDB()
        {
            _db = new Database();
            cmd = new MySqlCommand();
        }

        public void CreateNewInterviewScheme(InterviewScheme interview)
        {
            string query = "INSERT INTO UCL_InterviewScheme(Comment) VALUES (@Comment)";
            string query2 = "INSERT INTO UCL_InterviewSchemeForCountry(Country, InterviewScheme) VALUES (@CountryId, @InterviewScheme)";
            string param1 = "@Comment";
            string param2 = "@CountryId";
            string param3 = "@InterviewScheme";
            
            ExecuteTrans(query, query2, SetParameterWithValue(param1, interview.Comment), param2, param3, interview);
        }

        public void AddQuestion(Question question)
        {
            //string param1 = "@Id";
            //string param2 = "@Question";
            //string param3 = "@qId";
            //Guid newId = new Guid();
            //string query = "INSERT INTO UCL_QUESTION(Id, Question) VALUES (@Id, @Question)";
            //string query2 = "UPDATE UCL_INTERVIEWSCHEME SET qId = @qId WHERE Id = @Id";

            //ExecuteCmd(query, SetParameterWithValue(param1, newId), SetParameterWithValue(param2, question));
            //ExecuteCmd(query2, SetParameterWithValue(param3, newId), SetParameterWithValue(param1, id));
        }

        public void RemoveQuestion(IQuestion question)
        {
            throw new NotImplementedException();
        }

        public IInterviewScheme GetInterviewScheme(int id)
        {
            string query = "SELECT * FROM UCL_INTERVIEWSCHEME WHERE Id = @Id";

            MySqlCommand cmd = new MySqlCommand();
            using (cmd.Connection = new MySqlConnection(_connectionString))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;
                cmd.Connection.Open();
                try
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            _scheme = new InterviewScheme();
                            _scheme.Id = reader.GetInt32(0);
                            _scheme.CreatedDate = reader.GetDateTime(1);
                            _scheme.EditedDate = reader.GetDateTime(2);
                            _scheme.Comment = reader.GetString(3);
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
            return _scheme;
        }

        public List<IQuestion> GetAllSchemeQuestions(int id)
        {
            string query = "SELECT * FROM UCL_QUESTIONS WHERE Id = @Id";
            ExecureReader(query);
            return new List<IQuestion>();
        }

        public void UpdateInterviewScheme(int id)
        {
            string param1 = "@Id";
            string query = "UPDATE UCL_INTERVIEWSCHEME ";

            ExecuteCmd(query, SetParameterWithValue(param1, id));
        }

        public void DeleteInterviewScheme(int id)
        {
            string param1 = "@Id";
            string query = "DELETE FROM UCL_INTERVIEWSCHEME WHERE Id = @id";

            ExecuteCmd(query, SetParameterWithValue(param1, id));
        }

        #region Functionality
        private void ExecureReader(string query)
        {
            using (cmd.Connection = new MySqlConnection(_connectionString))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;
                cmd.Connection.Open();
                try
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            _scheme = new InterviewScheme();

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
        }

        private void ExecuteTrans(string query, string query2, MySqlParameter param, string param2, string param3, object value)
        {
            _db.Get_Connection();

            // Must assign both transaction object and connection
            // to Command object for a pending local transaction
            cmd.Connection = _db.connection;
            cmd.Connection.Open();
            cmd.Transaction = _db.connection.BeginTransaction();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = query;
            cmd.Parameters.Add(param);
            try
            {
                cmd.ExecuteNonQuery();
                var schemeId = cmd.LastInsertedId;
                var type = value as InterviewScheme;
                for (int i = 0; i < type.SchemeForCountries.Count; i++)
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = query2;
                    cmd.Parameters.Add(SetParameterWithValue(param2, type.SchemeForCountries[i].Id));
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
                cmd.Connection.Open();
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
                    cmd.Connection.Close();
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
                cmd.Connection.Open();
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
                    cmd.Connection.Close();
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
