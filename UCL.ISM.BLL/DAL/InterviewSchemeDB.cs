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

        public int CreateNewInterviewScheme(InterviewScheme interview)
        {
            string query = "INSERT INTO UCL_InterviewScheme(Comment) VALUES (@Comment)";
            string query2 = "INSERT INTO UCL_InterviewSchemeForCountry(Country, InterviewScheme) VALUES (@CountryId, @InterviewScheme)";
            string param1 = "@Comment";
            string param2 = "@CountryId";
            string param3 = "@InterviewScheme";

            return ExecuteTrans(query, query2, _db.SetParameterWithValue(param1, interview.Comment), param2, param3, interview);
        }

        public void AddQuestion(Question question)
        {
            string query = "INSERT INTO UCL_Question(Id, Question, InterviewScheme) VALUES (@Id, @Question, @InterviewScheme)";
            string param1 = "@Id";
            string param2 = "@Question";
            string param3 = "@InterviewScheme";

            List<MySqlParameter> temp = new List<MySqlParameter>();
            temp.Add(_db.SetParameterWithValue(param1, Guid.NewGuid()));
            temp.Add(_db.SetParameterWithValue(param2, question.Quest));
            temp.Add(_db.SetParameterWithValue(param3, question.InterviewSchemeId));

            _db.ExecuteCmd(query, temp);
        }


        public void RemoveQuestion(Question question)
        {
            throw new NotImplementedException();
        }

        public InterviewScheme GetInterviewScheme(int? id)
        {
            string query = "SELECT * FROM UCL_InterviewScheme WHERE Id = @Id";

            return ExecuteReaderScheme(query);
        }

        public List<InterviewScheme> GetAllInterviewSchemes()
        {
            string query = "SELECT * FROM UCL_InterviewScheme";

            return ExecuteReaderListScheme(query);
        }

        public List<Question> GetAllSchemeQuestions(int? id)
        {
            string query = "SELECT * FROM UCL_Question WHERE InterviewScheme =" + id;

            return ExecuteReaderQuestions(query);
        }

        public void UpdateInterviewScheme(int? id)
        {
            string param1 = "@Id";
            string query = "UPDATE UCL_InterviewScheme WHERE Id = @Id";

            _db.ExecuteCmd(query, _db.SetParameterWithValue(param1, id));
        }

        public void DeleteInterviewScheme(int? id)
        {
            string param1 = "@Id";
            string query = "DELETE FROM UCL_InterviewScheme WHERE Id = @id";

            _db.ExecuteCmd(query, _db.SetParameterWithValue(param1, id));
        }

        #region Functionality
        private List<Question> ExecuteReaderQuestions(string query)
        {
            _db.Get_Connection();
            List<Question> temp = new List<Question>();

            using (cmd.Connection = _db.conn)
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;

                try
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        Question quest;

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
                    if (_db.conn.State == System.Data.ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }
                }
            }
            return temp;
        }

        private InterviewScheme ExecuteReaderScheme(string query)
        {
            _db.Get_Connection();
            InterviewScheme scheme = new InterviewScheme();

            using (cmd.Connection = _db.conn)
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
                    if (_db.conn.State == System.Data.ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }
                }
                return scheme;
            }
        }

        private List<InterviewScheme> ExecuteReaderListScheme(string query)
        {
            _db.Get_Connection();
            InterviewScheme scheme;
            List<InterviewScheme> list = new List<InterviewScheme>();

            using (cmd.Connection = _db.conn)
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;

                try
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            scheme = new InterviewScheme();

                            scheme.Id = reader.GetInt32(0);
                            scheme.CreatedDate = reader.GetDateTime(1);
                            scheme.EditedDate = reader.GetDateTime(2);
                            scheme.Name = reader.GetString(3).ToString();
                            scheme.Comment = reader.GetString(4).ToString();

                            list.Add(scheme);
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    if (_db.conn.State == System.Data.ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }
                }
            }
            return list;
        }

        private int ExecuteTrans(string query, string query2, MySqlParameter param, string param2, string param3, object value)
        {
            var schemeId = 0;
            _db.Get_Connection();

            // Must assign both transaction object and connection
            // to Command object for a pending local transaction
            cmd.Connection = _db.conn;
            cmd.Transaction = _db.conn.BeginTransaction();
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
                    cmd.Parameters.Add(_db.SetParameterWithValue(param2, type.CountryId[i]));
                    cmd.Parameters.Add(_db.SetParameterWithValue(param3, schemeId));
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
        #endregion
    }
}
