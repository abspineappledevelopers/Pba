using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using UCL.ISM.BLL.BLL;

namespace UCL.ISM.BLL.DAL
{
    class NationalityDB
    {
        private Database db = new Database();
        List<Nationality> _listna;
        Nationality _na;

        public List<Nationality> GetAllNationalities()
        {
            List<StudyField> list = new List<StudyField>();

            db.Get_Connection();
            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = db.connection;

            try
            {
                cmd.CommandText = "SELECT * FROM UCL_Nationality";

                MySqlDataReader reader = cmd.ExecuteReader();
                _listna = new List<Nationality>();

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
                if (db.connection.State == System.Data.ConnectionState.Open)
                {
                    db.CloseConnection();
                }
            }

            return _listna;
        }
    }
}
