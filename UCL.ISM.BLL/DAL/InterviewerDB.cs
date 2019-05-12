using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using UCL.ISM.BLL.BLL;

namespace UCL.ISM.BLL.DAL
{
    class InterviewerDB
    {
        private Database db = new Database();
        List<Interviewer> _listin;
        Interviewer _in;

        public List<Interviewer> GetAllInterviewers()
        {
            List<Interviewer> list = new List<Interviewer>();

            db.Get_Connection();

            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = db.connection;

            try
            {
                cmd.CommandText = "SELECT * FROM UCL_Interviewer";

                MySqlDataReader reader = cmd.ExecuteReader();
                _listin = new List<Interviewer>();

                while (reader.Read())
                {
                    _in = new Interviewer();
                    _in.Id = reader.GetString(0).ToString();
                    _in.Firstname = reader.GetString(1).ToString();
                    _in.Lastname = reader.GetString(2).ToString();

                    _listin.Add(_in);
                }
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
                    db.CloseConnection();
                }
            }

            return _listin;
        }
    }
}
