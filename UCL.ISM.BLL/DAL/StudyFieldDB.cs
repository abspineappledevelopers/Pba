using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using UCL.ISM.BLL.DAL;
using UCL.ISM.BLL.Interface;

namespace UCL.ISM.BLL
{
    class StudyFieldDB
    {
        private Database _db;
        private MySqlCommand cmd;
        StudyField _sf;
        List<StudyField> _listsf;

        public StudyFieldDB()
        {
            _db = new Database();
            cmd = new MySqlCommand();
        }

        public void CreateNewStudyField(string fieldName)
        {
            string query = "INSERT INTO UCL_StudyField(Name) VALUES (@Name)";
            string param1 = "@Name";

            _db.ExecuteCmd(query, _db.SetParameterWithValue(param1, fieldName));
        }

        public StudyField GetStudyField(int Id)
        {
            string query = "SELECT * FROM UCL_StudyField WHERE Id = @Id";
            throw new NotImplementedException();
        }

        public List<StudyField> GetAllStudyFields()
        {
            string query = "SELECT * FROM UCL_StudyField";

            return ExecuteReaderStudyFields(query);
        }

        public void EditStudyField(StudyField studyField)
        {
            string query = "UPDATE UCL_StudyField SET Name = @Name WHERE Id = @id;";
            string param1 = "@id";
            string param2 = "@Name";

            _db.ExecuteCmd(query, _db.SetParameterWithValue(param1, studyField.Id), _db.SetParameterWithValue(param2, studyField.FieldName));
        }

        public void DeleteStudyField(int Id)
        {
            string query = "DELETE FROM UCL_StudyField WHERE Id = @id;";
            string param1 = "@id";

            _db.ExecuteCmd(query, _db.SetParameterWithValue(param1, Id));
        }

        #region Functionality
        private List<StudyField> ExecuteReaderStudyFields(string query)
        {
            _db.Get_Connection();
            List<StudyField> list = new List<StudyField>();

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
                            StudyField sf = new StudyField();
                            sf.Id = Convert.ToInt32(reader.GetInt32(0));
                            sf.FieldName = reader.GetString(1).ToString();
                            sf.Created = reader.GetDateTime(2);
                            sf.Edited = reader.GetDateTime(3);

                            list.Add(sf);
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
        #endregion
    }
}
