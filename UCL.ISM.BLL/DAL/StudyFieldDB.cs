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
        List<IStudyField> _listsf;

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

        public IStudyField GetStudyField(int Id)
        {
            string query = "SELECT * FROM UCL_StudyField WHERE Id = @Id";
            throw new NotImplementedException();
        }

        public List<IStudyField> GetAllStudyFields()
        {
            string query = "SELECT * FROM UCL_StudyField";

            return ExecuteReaderStudyFields(query);
        }

        public void EditStudyField(IStudyField studyField)
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
        private List<IStudyField> ExecuteReaderStudyFields(string query)
        {
            _db.Get_Connection();
            List<IStudyField> list = new List<IStudyField>();

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
                            IStudyField sf = new StudyField();
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
