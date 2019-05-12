using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using UCL.ISM.BLL.BLL;

namespace UCL.ISM.BLL.DAL
{
    public class ApplicantDB
    {
        private Database db = new Database();
        Applicant _app;
        List<Applicant> _listapp;

        public void CreateApplicant(Applicant applicant)
        {
            db.Get_Connection();
            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = db.connection;

            try
            {
                cmd.CommandText = "INSERT INTO UCL_Applicant(Firstname, Lastname, Email, Age, IsEU, StudyField, Priority, Nationality, Interviewer) VALUES (@Name)";
                cmd.Parameters.Add("@Firstname", MySqlDbType.VarChar, 60);
                cmd.Parameters.Add("@Lastname", MySqlDbType.VarChar, 60);
                cmd.Parameters.Add("@Email", MySqlDbType.VarChar, 100);
                cmd.Parameters.Add("@Age", MySqlDbType.Int32, 3);
                cmd.Parameters.Add("@IsEU", MySqlDbType.Bit);
                cmd.Parameters.Add("@StudyField", MySqlDbType.Int32, 11);
                cmd.Parameters.Add("@Priority", MySqlDbType.Int32, 3);
                cmd.Parameters.Add("@Nationality", MySqlDbType.Int32, 11);
                cmd.Parameters.Add("@Interviewer", MySqlDbType.VarChar, 128);

                cmd.Parameters["@Firstname"].Value = applicant.Firstname;
                cmd.Parameters["@Lastname"].Value = applicant.Lastname;
                cmd.Parameters["@Email"].Value = applicant.Email;
                cmd.Parameters["@Age"].Value = applicant.Age;
                cmd.Parameters["@IsEU"].Value = applicant.IsEU;
                cmd.Parameters["@StudyField"].Value = applicant.StudyField.Id;
                cmd.Parameters["@Priority"].Value = applicant.Priority;
                cmd.Parameters["@Nationality"].Value = applicant.Nationality.Id;
                cmd.Parameters["@Interviewer"].Value = applicant.Interviewer.Id;

                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
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
    }
}
