using System;
using System.Collections.Generic;
using System.Text;
using UCL.ISM.BLL.Interface;
using UCL.ISM.BLL.BLL;
using MySql.Data.MySqlClient;

namespace UCL.ISM.BLL.DAL
{
    public class InterviewSchemeDB
    {
        private readonly string _connectionString;
        private readonly List<IQuestion> _questions;
        private IInterviewScheme _scheme;

        internal List<IQuestion> Questions => _questions;

        public InterviewSchemeDB()
        {
            _questions = new List<IQuestion>();
        }

        public void CreateNewInterviewScheme(int id)
        {
            string param1 = "@Id";
            string query = "INSERT INTO UCL_INTERVIEWSCHEME(Id) VALUES (@Id)";

            MySqlCommand cmd = new MySqlCommand();
            using (cmd.Connection = new MySqlConnection(_connectionString))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;
                cmd.Parameters.Add(param1, MySqlDbType.Guid);
                cmd.Parameters[param1].Value = id;
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

        public void AddQuestion(int id, string question)
        {
            string param1 = "@Id";
            string param2 = "@Question";
            string param3 = "@qId";
            Guid newId = new Guid();
            string query = "INSERT INTO UCL_QUESTION(Id, Question) VALUES (@Id, @Question)";
            string query2 = "UPDATE UCL_INTERVIEWSCHEME SET qId = @qId WHERE Id = @Id";

            MySqlCommand cmd = new MySqlCommand();
            using (cmd.Connection = new MySqlConnection(_connectionString))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;
                cmd.Parameters.Add(param1, MySqlDbType.Guid);
                cmd.Parameters.Add(param2, MySqlDbType.VarChar, 1900);
                cmd.Parameters[param1].Value = newId;
                cmd.Parameters[param2].Value = question;
                cmd.Connection.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {

                    throw;
                }

                cmd.CommandText = query2;
                cmd.Parameters.Add(param3, MySqlDbType.Guid);
                cmd.Parameters.Add(param1, MySqlDbType.Guid);
                cmd.Parameters[param3].Value = newId;
                cmd.Parameters[param1].Value = id;
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

        public IInterviewScheme GetInterviewScheme()
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

        private List<IQuestion> GetAllSchemeQuestions()
        {
            string param1 = "@Id";
            string query = "";


            return Questions;
        }

        public void UpdateInterviewScheme(int id)
        {
            string param1 = "@Id";
            string query = "UPDATE UCL_INTERVIEWSCHEME ";

            ExecureCmd(query, id, param1);
        }

        public void DeleteInterviewScheme(int id)
        {
            string param1 = "@Id";
            string query = "DELETE FROM UCL_INTERVIEWSCHEME WHERE Id = @id";

            ExecureCmd(query, id, param1);
        }

        private void ExecureCmd(string query, int id, string param1, string param2 = null)
        {
            MySqlCommand cmd = new MySqlCommand();
            using (cmd.Connection = new MySqlConnection(_connectionString))
            {
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;
                if (param2 != null)
                {

                }
                else
                {
                    cmd.Parameters.Add(param1, MySqlDbType.Int32);
                    cmd.Parameters[param1].Value = id;
                }
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
    }
}
