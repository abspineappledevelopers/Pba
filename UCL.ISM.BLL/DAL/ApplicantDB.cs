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
                    "UCL_StudyField.Name, UCL_Interviewer.Id, UCL_Interviewer.Firstname, UCL_Interviewer.Lastname, UCL_Applicant.Comment, UCL_Applicant.ResidencePermit, UCL_InterviewScheme.Name FROM UCL_Applicant JOIN UCL_Nationality " +
                    "on UCL_Applicant.Nationality = UCL_Nationality.Id JOIN UCL_StudyField on UCL_Applicant.StudyField = UCL_StudyField.Id LEFT OUTER JOIN UCL_Interviewer " +
                    "on UCL_Applicant.Interviewer = UCL_Interviewer.Id LEFT OUTER JOIN UCL_InterviewScheme on UCL_Applicant.InterviewAssigned = UCL_InterviewScheme.Id"
                    + " WHERE UCL_Applicant.InterviewAssigned is NULL OR UCL_Applicant.Interviewer is NULL";

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
                        HasResidencePermit = reader.GetBoolean(16)
                    };

                    if(reader.GetValue(15) != DBNull.Value)
                    {
                        _app.Comment = reader.GetString(15).ToString();
                    }

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
                    "UCL_StudyField.Name, UCL_Interviewer.Id, UCL_Interviewer.Firstname, UCL_Interviewer.Lastname, UCL_Applicant.Comment, UCL_Applicant.ResidencePermit, UCL_Applicant.InterviewAssigned, UCL_InterviewScheme.Name FROM UCL_Applicant JOIN UCL_Nationality " +
                    "on UCL_Applicant.Nationality = UCL_Nationality.Id JOIN UCL_StudyField on UCL_Applicant.StudyField = UCL_StudyField.Id LEFT OUTER JOIN UCL_Interviewer " +
                    "on UCL_Applicant.Interviewer = UCL_Interviewer.Id LEFT OUTER JOIN UCL_InterviewScheme on UCL_InterviewScheme.Id = UCL_Applicant.InterviewAssigned WHERE UCL_Applicant.Id = @Id";
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

        public Applicant EditApplicant(Applicant applicant)
        {
            string query = "UPDATE UCL_Applicant SET Firstname = @Firstname, Lastname = @Lastname, Email = @Email, Age = @Age, IsEU = @IsEU, StudyField = @StudyField, Priority = @Priority, Nationality = @Nationality, Interviewer = @Interviewer, Comment = @Comment, ResidencePermit = @Residence, InterviewAssigned = @Scheme WHERE Id = @Id";
            string id = "@Id";
            string firstname = "@Firstname";
            string lastname = "@Lastname";
            string email = "@Email";
            string age = "@Age";
            string iseu = "@IsEU";
            string studyfield = "@StudyField";
            string priority = "@Priority";
            string nationality = "@Nationality";
            string interviewer = "@Interviewer";
            string comment = "@Comment";
            string residencepermit = "@Residence";
            string interviewscheme = "@Scheme";

            List<string> tempP = new List<string>() {
                id, firstname, lastname, email, age, iseu, studyfield, priority, nationality, interviewer, comment, residencepermit, interviewscheme
            };

            List<object> tempV = new List<object>()
            {
                applicant.Id, applicant.Firstname, applicant.Lastname, applicant.Email, applicant.Age,
                applicant.IsEU, applicant.StudyField.Id, applicant.Priority, applicant.Nationality.Id,
                applicant.Interviewer.Id, applicant.Comment, applicant.HasResidencePermit, applicant.InterviewScheme.Id
            };

            ExecuteCmd(query, SetParametersList(tempP, tempV));

            return applicant;
        }

        public List<Applicant> GetAllApplicantsLimitedData()
        {
            db.Get_Connection();
            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = db.conn;

            try
            {
                cmd.CommandText = "SELECT UCL_Applicant.Id, UCL_Applicant.Firstname, UCL_Applicant.Lastname, UCL_ApplicantProcess.Id," +
                "UCL_ApplicantProcess.Process, UCL_Interviewer.Firstname, UCL_Interviewer.Lastname FROM UCL_Applicant JOIN UCL_ApplicantProcess ON UCL_Applicant.Process = UCL_ApplicantProcess.Id LEFT OUTER JOIN UCL_Interviewer ON UCL_Interviewer.Id = UCL_Applicant.Interviewer";
      

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

                    if (_app.Process.Id != 1)
                    {
                        _app.Interviewer = new Interviewer()
                        {
                            Firstname = reader.GetString(5).ToString(),
                            Lastname = reader.GetString(6).ToString()
                        };
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

        public List<Applicant> GetAllApplicantsForInterviewer(string id)
        {
            List<Applicant> list = new List<Applicant>();
            string query = "SELECT * FROM UCL_Applicant JOIN UCL_StudyField ON UCL_Applicant.StudyField = UCL_StudyField.Id JOIN UCL_Nationality ON UCL_Applicant.Nationality = UCL_Nationality.Id JOIN UCL_ApplicantProcess ON UCL_Applicant.Process = UCL_ApplicantProcess.Id WHERE UCL_Applicant.Interviewer = @Id";
            List<object[]> temp = ExecuteReaderList(query, id);
            foreach (object[] item in temp)
            {
                list.Add(new Applicant {
                    Id = Guid.Parse(item[0].ToString()),
                    Firstname = item[1].ToString(),
                    Lastname = item[2].ToString(),
                    Email = item[3].ToString(),
                    Age = Convert.ToInt32(item[4]),
                    IsEU = Convert.ToBoolean(item[20]),
                    StudyField = new StudyField() { Id = Convert.ToInt32(item[14]), FieldName = item[15].ToString() },
                    Priority = Convert.ToInt32(item[7]),
                    Nationality = new Nationality() { Id = Convert.ToInt32(item[18]), Name = item[19].ToString(), IsEU = Convert.ToBoolean(item[20]) },
                    Interviewer = new Interviewer() { Id = item[9].ToString() },
                    Comment = item[11].ToString(),
                    HasResidencePermit = Convert.ToBoolean(item[12]),
                    Process = new ApplicantProcess() { Id = Convert.ToInt32(item[21]), Process = item[22].ToString() }
                });
            }
            return list;
        }
        
        public void CreateApplicant(Applicant applicant)
        {
            string query = "INSERT INTO UCL_Applicant(Id, Firstname, Lastname, Email, Age, IsEU, StudyField, Priority, Nationality, Interviewer, Comment, Process) VALUES (@Id, @Firstname, @Lastname, @Email, @Age, @IsEU, @StudyField, @Priority, @Nationality, @Interviewer, @Comment, @Process)";
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
            string param12 = "@Process";

            List<string> tempP = new List<string>() {
                param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12
            };

            List<object> tempV = new List<object>()
            {
                applicant.Id, applicant.Firstname, applicant.Lastname, applicant.Email, applicant.Age,
                applicant.IsEU, applicant.StudyField.Id, applicant.Priority, applicant.Nationality.Id,
                applicant.Interviewer.Id, applicant.Comment, 1
            };

            ExecuteCmd(query, SetParametersList(tempP, tempV));
        }

        public void AddInterviewSchemeToApplicant(Applicant model)
        {
            string query = "UPDATE UCL_Applicant SET InterviewAssigned = @Scheme WHERE Id = @Id; UPDATE UCL_Applicant SET Process = 2 WHERE Id = @Id AND Interviewer IS NOT NULL";
            string param1 = "@Id";
            string param2 = "@Scheme";
            ExecuteCmd(query, SetParameterWithValue(param1, model.Id), SetParameterWithValue(param2, model.InterviewScheme.Id));
        }

        public void AddInterviewerToApplicant(Applicant model)
        {
            string query = "UPDATE UCL_Applicant SET Interviewer=@Interviewer WHERE Id = @Id; UPDATE UCL_Applicant SET Process = 2 WHERE Id = @Id AND InterviewAssigned IS NOT NULL";
            string param1 = "@Id";
            string param2 = "@Interviewer";
            ExecuteCmd(query, SetParameterWithValue(param1, model.Id), SetParameterWithValue(param2, model.Interviewer.Id));
        }
    }
}
