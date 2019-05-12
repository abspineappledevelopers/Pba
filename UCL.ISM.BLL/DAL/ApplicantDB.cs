﻿using MySql.Data.MySqlClient;
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

        public List<Applicant> GetAllApplicantsWithoutSchema()
        {
            db.Get_Connection();
            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = db.connection;

            try
            {
                cmd.CommandText = "SELECT UCL_Applicant.ID, UCL_Applicant.Firstname, UCL_Applicant.Lastname, UCL_Applicant.Email, UCL_Applicant.Age,"+ 
                    "UCL_Applicant.IsEU, UCL_Applicant.Priority, UCL_Nationality.Id, UCL_Nationality.Name, UCL_Nationality.IsEU, UCL_StudyField.Id," + 
                    "UCL_StudyField.Name, UCL_Interviewer.Id, UCL_Interviewer.Firstname, UCL_Interviewer.Lastname FROM UCL_Applicant JOIN UCL_Nationality "+ 
                    "on UCL_Applicant.Nationality = UCL_Nationality.Id JOIN UCL_StudyField on UCL_Applicant.StudyField = UCL_StudyField.Id JOIN UCL_Interviewer "+
                    "on UCL_Applicant.Interviewer = UCL_Interviewer.Id WHERE UCL_Applicant.InterviewAssigned = 0";

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
                        Nationality = new Nationality() { Id = reader.GetInt32(7), Name = reader.GetString(8).ToString(), IsEU = reader.GetBoolean(9)},
                        StudyField = new StudyField() { Id = reader.GetInt32(10), FieldName = reader.GetString(11).ToString() },
                        Interviewer = new Interviewer() { Id = reader.GetString(12).ToString(), Firstname = reader.GetString(13).ToString(), Lastname = reader.GetString(14).ToString()}
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
            db.Get_Connection();
            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = db.connection;

            try
            {
                cmd.CommandText = "INSERT INTO UCL_Applicant(Id, Firstname, Lastname, Email, Age, IsEU, StudyField, Priority, Nationality, Interviewer) VALUES (@Id, @Firstname, @Lastname, @Email, @Age, @IsEU, @StudyField, @Priority, @Nationality, @Interviewer)";
                cmd.Parameters.Add("@Id", MySqlDbType.Guid);
                cmd.Parameters.Add("@Firstname", MySqlDbType.VarChar, 60);
                cmd.Parameters.Add("@Lastname", MySqlDbType.VarChar, 60);
                cmd.Parameters.Add("@Email", MySqlDbType.VarChar, 100);
                cmd.Parameters.Add("@Age", MySqlDbType.Int32, 3);
                cmd.Parameters.Add("@IsEU", MySqlDbType.Bit);
                cmd.Parameters.Add("@StudyField", MySqlDbType.Int32, 11);
                cmd.Parameters.Add("@Priority", MySqlDbType.Int32, 3);
                cmd.Parameters.Add("@Nationality", MySqlDbType.Int32, 11);
                cmd.Parameters.Add("@Interviewer", MySqlDbType.Guid);

                cmd.Parameters["@Id"].Value = applicant.Id;
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