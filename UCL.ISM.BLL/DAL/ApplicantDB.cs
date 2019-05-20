using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using UCL.ISM.BLL.BLL;
using UCL.ISM.BLL.Interface;

namespace UCL.ISM.BLL.DAL
{
    public class ApplicantDB : MySqlExtension<Applicant>
    {
        private Database db = new Database();
        Applicant _app;
        List<Applicant> _listapp;

        public List<Applicant> GetAllApplicantsWithoutSchemaOrInterviewer()
        {
            string query = "SELECT UCL_Applicant.ID, UCL_Applicant.Firstname, UCL_Applicant.Lastname, UCL_Applicant.Email, UCL_Applicant.Age," +
                    "UCL_Applicant.IsEU, UCL_Applicant.Priority, UCL_Nationality.Id, UCL_Nationality.Name, UCL_Nationality.IsEU, UCL_StudyField.Id," +
                    "UCL_StudyField.Name, UCL_Interviewer.Id, UCL_Interviewer.Firstname, UCL_Interviewer.Lastname, UCL_Applicant.Comment, UCL_Applicant.ResidencePermit FROM UCL_Applicant JOIN UCL_Nationality " +
                    "on UCL_Applicant.Nationality = UCL_Nationality.Id JOIN UCL_StudyField on UCL_Applicant.StudyField = UCL_StudyField.Id LEFT OUTER JOIN UCL_Interviewer " +
                    "on UCL_Applicant.Interviewer = UCL_Interviewer.Id WHERE UCL_Applicant.InterviewAssigned is NULL";

            db.Get_Connection();
            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = db.conn;

            try
            {
                cmd.CommandText = query;

                MySqlDataReader reader = cmd.ExecuteReader();
                _listapp = new List<Applicant>();

                while (reader.Read())
                {
                    _app = new Applicant()
                    {
                        Id = reader.GetGuid(0),
                        Firstname = reader.GetString(1).ToString(),
                        Lastname = reader.GetString(2).ToString(),
                        Email = reader.GetString(3).ToString(),
                        Age = reader.GetInt32(4),
                        IsEU = reader.GetBoolean(5),
                        Priority = reader.GetInt32(6),
                        Nationality = new Nationality() { Id = reader.GetInt32(7), Name = reader.GetString(8).ToString(), IsEU = reader.GetBoolean(9) },
                        StudyField = new StudyField() { Id = reader.GetInt32(10), FieldName = reader.GetString(11).ToString() },
                        Comment = reader.GetString(15).ToString(),
                        HasResidencePermit = reader.GetBoolean(16)
                    };

                    if(reader.GetValue(12) != DBNull.Value)
                    {
                        _app.Interviewer = new Interviewer() { Id = reader.GetString(12).ToString(), Firstname = reader.GetString(13).ToString(), Lastname = reader.GetString(14).ToString() };
                    }

                    if(reader.GetValue(17) != DBNull.Value)
                    {
                        _app.InterviewScheme = new InterviewScheme() { Name = reader.GetString(17).ToString() };
                    }

                    _listapp.Add(_app);
                }

                return _listapp;
            }
            catch (Exception)
            {
                db.CloseConnection();

                throw;
            }
            finally
            {
                if (db.conn.State == System.Data.ConnectionState.Open)
                {
                    db.conn.Close();
                }
            }
        }

        public Applicant GetApplicant(string id)
        {
            string query = "SELECT UCL_Applicant.Id, UCL_Applicant.Firstname, UCL_Applicant.Lastname, UCL_Applicant.Email, UCL_Applicant.Age," +
                    "UCL_Applicant.IsEU, UCL_Applicant.Priority, UCL_Nationality.Id, UCL_Nationality.Name, UCL_Nationality.IsEU, UCL_StudyField.Id," +
                    "UCL_StudyField.Name, UCL_Interviewer.Id, UCL_Interviewer.Firstname, UCL_Interviewer.Lastname, UCL_Applicant.Comment, UCL_Applicant.ResidencePermit, UCL_Applicant.InterviewAssigned FROM UCL_Applicant JOIN UCL_Nationality " +
                    "on UCL_Applicant.Nationality = UCL_Nationality.Id JOIN UCL_StudyField on UCL_Applicant.StudyField = UCL_StudyField.Id LEFT OUTER JOIN UCL_Interviewer " +
                    "on UCL_Applicant.Interviewer = UCL_Interviewer.Id WHERE UCL_Applicant.Id = @Id";
            string param1 = "@Id";

            db.Get_Connection();
            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = db.conn;

            try
            {
                cmd.CommandText = query;

                cmd.Parameters.Add("@Id", MySqlDbType.Guid);
                cmd.Parameters["@Id"].Value = id;

                MySqlDataReader reader = cmd.ExecuteReader();
                _listapp = new List<Applicant>();

                while (reader.Read())
                {
                    _app = new Applicant()
                    {
                        Id = reader.GetGuid(0),
                        Firstname = reader.GetString(1).ToString(),
                        Lastname = reader.GetString(2).ToString(),
                        Email = reader.GetString(3).ToString(),
                        Age = reader.GetInt32(4),
                        IsEU = reader.GetBoolean(5),
                        Priority = reader.GetInt32(6),
                        Nationality = new Nationality() { Id = reader.GetInt32(7), Name = reader.GetString(8).ToString(), IsEU = reader.GetBoolean(9) },
                        StudyField = new StudyField() { Id = reader.GetInt32(10), FieldName = reader.GetString(11).ToString() },
                        Comment = reader.GetString(15).ToString(),
                        HasResidencePermit = reader.GetBoolean(16)
                    };

                    if (reader.GetValue(12) != DBNull.Value)
                    {
                        _app.Interviewer = new Interviewer() { Id = reader.GetString(12).ToString(), Firstname = reader.GetString(13).ToString(), Lastname = reader.GetString(14).ToString() };
                    }
                    if(reader.GetValue(17) != DBNull.Value)
                    {
                        _app.InterviewScheme = new InterviewScheme() { Id = reader.GetInt32(17) };
                    }
                }

                return _app;
            }
            catch (Exception)
            {
                db.CloseConnection();

                throw;
            }
            finally
            {
                if (db.conn.State == System.Data.ConnectionState.Open)
                {
                    db.conn.Close();
                }
            }
        }

        public List<Applicant> GetAllApplicantsLimitedData()
        {
            db.Get_Connection();
            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = db.connection;

            try
            {
                cmd.CommandText = "SELECT UCL_Applicant.Id, UCL_Applicant.Firstname, UCL_Applicant.Lastname, UCL_ApplicantProcess.Id," + 
                "UCL_ApplicantProcess.Process FROM UCL_Applicant JOIN UCL_ApplicantProcess ON UCL_Applicant.Process = UCL_ApplicantProcess.Id";
      

                MySqlDataReader reader = cmd.ExecuteReader();
                _listapp = new List<Applicant>();
                

                while (reader.Read())
                {
                    _app = new Applicant()
                    {
                        Id = reader.GetGuid(0),
                        Firstname = reader.GetString(1).ToString(),
                        Lastname = reader.GetString(2).ToString(),

                        Process = new ApplicantProcess()
                        {
                            Id = reader.GetInt32(3),
                            Process = reader.GetString(4).ToString()
                        }
                    };

                    _listapp.Add(_app);
                }

                return _listapp;
            }
            catch (Exception)
            {
                db.CloseConnection();

                throw;
            }
            finally
            {
                if (db.connection.State == System.Data.ConnectionState.Open)
                {
                    db.connection.Close();
                }
            }
        }

        public void CreateApplicant(Applicant applicant)
        {
            string query = "INSERT INTO UCL_Applicant(Id, Firstname, Lastname, Email, Age, IsEU, StudyField, Priority, Nationality, Interviewer, Comment) VALUES (@Id, @Firstname, @Lastname, @Email, @Age, @IsEU, @StudyField, @Priority, @Nationality, @Interviewer, @Comment)";
            string param1 = "@Id";
            string param2 = "@Firstname";
            string param3 = "@Lastname";
            string param4 = "@Email";
            string param5 = "@Age";
            string param6 = "@IsEU";
            string param7 = "@StudyField";
            string param8 = "@Priority";
            string param9 = "@Nationality";
            string param10 = "@Interviewer";
            string param11 = "@Comment";

            List<string> tempP = new List<string>() {
                param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11
            };

            List<object> tempV = new List<object>()
            {
                applicant.Id, applicant.Firstname, applicant.Lastname, applicant.Email, applicant.Age,
                applicant.IsEU, applicant.StudyField.Id, applicant.Priority, applicant.Nationality.Id,
                applicant.Interviewer.Id, applicant.Comment
            };

            ExecuteCmd(query, SetParametersList(tempP, tempV));
        }

        public void AddInterviewSchemeToApplicant(Applicant model)
        {
            string query = "UPDATE UCL_Applicant SET InterviewAssigned = @Scheme WHERE Id = @Id";
            string param1 = "@Id";
            string param2 = "@Scheme";
            ExecuteCmd(query, SetParameterWithValue(param1, model.Id), SetParameterWithValue(param2, model.InterviewScheme.Id));
        }

        public void AddInterviewerToApplicant(Applicant model)
        {
            string query = "UPDATE UCL_Applicant SET Interviewer=@Interviewer WHERE Id = @Id";
            string param1 = "@Id";
            string param2 = "@Interviewer";
            ExecuteCmd(query, SetParameterWithValue(param1, model.Id), SetParameterWithValue(param2, model.Interviewer.Id));
        }
    }
}
