using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using UCL.ISM.BLL.DAL;

namespace UCL.ISM.BLL
{
    class StudyFieldDB
    {
        private Database db = new Database();
        StudyField _sf;
        List<StudyField> _listsf;

        public void CreateNewStudyField(string fieldName)
        {
            db.Get_Connection();
            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = db.connection;
            
            try
            {
                cmd.CommandText = "INSERT INTO UCL_StudyField(Name) VALUES (@Name)";
                cmd.Parameters.Add("@Name", MySqlDbType.VarChar, 50);
                cmd.Parameters["@Name"].Value = fieldName;
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

        public StudyField GetStudyField(int Id)
        {
            throw new NotImplementedException();
        }

        public List<StudyField> GetAllStudyFields()
        {
            List<StudyField> list = new List<StudyField>();

            db.Get_Connection();
            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = db.connection;

            try
            {
                cmd.CommandText = "SELECT * FROM UCL_StudyField";

                MySqlDataReader reader = cmd.ExecuteReader();
                _listsf = new List<StudyField>();

                while (reader.Read())
                {
                    _sf = new StudyField();
                    _sf.Id = Convert.ToInt32(reader.GetInt32(0));
                    _sf.FieldName = reader.GetString(1).ToString();
                    _sf.Created = reader.GetDateTime(2);
                    _sf.Edited = reader.GetDateTime(3);

                    _listsf.Add(_sf);
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

            return _listsf;
        }

        public void EditStudyField(StudyField studyField)
        {
            db.Get_Connection();

            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = db.connection;

            try
            {
                cmd.CommandText = "UPDATE UCL_StudyField SET Name = @Name WHERE Id = @id;";

                //Creating parameter objects
                cmd.Parameters.Add("@id", MySqlDbType.Int32, 11);
                cmd.Parameters.Add("@Name", MySqlDbType.VarChar, 50);

                //Adding values to parameter
                cmd.Parameters["@id"].Value = studyField.Id;
                cmd.Parameters["@Name"].Value = studyField.FieldName;

                cmd.ExecuteNonQuery();
            }
            catch
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
        }

        public void DeleteStudyField(int Id)
        {
            MySqlCommand cmd = new MySqlCommand();

            cmd.Connection = db.connection;

            try
            {
                cmd.CommandText = "DELETE FROM UCL_StudyField WHERE Id = @id;";

            //Creating parameter objects
                cmd.Parameters.Add("@id", MySqlDbType.Int32, 11);

                //Adding values to parameter
                cmd.Parameters["@id"].Value = Id;

                cmd.ExecuteNonQuery();
            }
            catch
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
        }
    }
}
