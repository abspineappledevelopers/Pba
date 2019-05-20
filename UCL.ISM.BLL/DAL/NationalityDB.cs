using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using UCL.ISM.BLL.BLL;
using UCL.ISM.BLL.Interface;

namespace UCL.ISM.BLL.DAL
{
    class NationalityDB
    {
        private Database db = new Database();
        List<INationality> _listna;
        Nationality _na;

        public bool IsEu(int id)
        {
            string query = "SELECT * FROM UCL_Nationality WHERE Id = '" + id + "';";
            db.Get_Connection();

            MySqlCommand cmd = new MySqlCommand();
            bool result = false;
            cmd.Connection = db.conn;

            try
            {
                cmd.CommandText = query;
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result = reader.GetBoolean(2);
                    /*_na = new Nationality();
                    _na.Id = Convert.ToInt32(reader.GetInt32(0));
                    _na.Name = reader.GetString(1).ToString();
                    _na.IsEU = reader.GetBoolean(2);*/
                }

                //result = _na.IsEU;
            }
            catch(Exception)
            {
                db.CloseConnection();

                throw;
            }
            finally
            {
                if (db.conn.State == System.Data.ConnectionState.Open)
                {
                    db.CloseConnection();
                }
            }

            return result;
        }

        public List<INationality> GetAllNationalities()
        {
            //List<StudyField> list = new List<StudyField>();  ???
            string query = "SELECT * FROM UCL_Nationality";
            db.Get_Connection();
            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = db.conn;

            try
            {
                cmd.CommandText = query;

                MySqlDataReader reader = cmd.ExecuteReader();
                _listna = new List<INationality>();

                while (reader.Read())
                {
                    _na = new Nationality();
                    _na.Id = Convert.ToInt32(reader.GetInt32(0));
                    _na.Name = reader.GetString(1).ToString();
                    _na.IsEU = reader.GetBoolean(2);

                    _listna.Add(_na);
                }
            }
            catch (Exception e)
            {
                db.CloseConnection();

                throw;
            }
            finally
            {
                if (db.conn.State == System.Data.ConnectionState.Open)
                {
                    db.CloseConnection();
                }
            }

            return _listna;
        }
    }
}
