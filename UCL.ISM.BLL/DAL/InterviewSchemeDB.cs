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

        public void CreateNewInterviewScheme(InterviewScheme interview)
        {
            _db = new Database();
            _db.Get_Connection();

            MySqlConnection conn = _db.connection;
            conn.Open();

            MySqlCommand cmd = new MySqlCommand();
            MySqlTransaction myTrans;

            // Start a local transaction
            myTrans = conn.BeginTransaction();
            // Must assign both transaction object and connection
            // to Command object for a pending local transaction
            cmd.Connection = conn;
            cmd.Transaction = myTrans;

            try
            {
                cmd.CommandText = "INSERT INTO UCL_InterviewScheme(Comment) VALUES (@Comment)";

                cmd.Parameters.Add("@Comment", MySqlDbType.VarChar, 1900);
                cmd.Parameters["@Comment"].Value = interview.Comment;

                cmd.ExecuteNonQuery();
                var schemeId = cmd.LastInsertedId;

                for (int i = 0; i < interview.SchemeForCountries.Count; i++)
                {
                    cmd.CommandText = "INSERT INTO UCL_InterviewSchemeForCountry(Country, InterviewScheme) VALUES (@CountryId, @InterviewScheme)";
                    cmd.Parameters.Add("@CountryId", MySqlDbType.Int32, 11);
                    cmd.Parameters.Add("@InterviewScheme", MySqlDbType.Int32, 11);
                    cmd.Parameters["@CountryId"].Value = interview.SchemeForCountries[i].Id;
                    cmd.Parameters["@InterviewScheme"].Value = schemeId;

                    cmd.ExecuteNonQuery();
                }

                myTrans.Commit();
            }
            catch (Exception e)
            {
                try
                {
                    myTrans.Rollback();
                }
                catch (SqlException ex)
                {
                    if (myTrans.Connection != null)
                    {

                    }
                }
            }
            finally
            {
                conn.Close();
            }
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

        private void ExecuteCmd(string query, MySqlParameter param1, MySqlParameter param2)
        {
            _db.Get_Connection();

            MySqlCommand cmd = new MySqlCommand();

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
            MySqlCommand cmd = new MySqlCommand();
            using (cmd.Connection = new MySqlConnection(_connectionString))
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
