using System.Collections.Generic;
using UCL.ISM.BLL.BLL;

namespace UCL.ISM.BLL.DAL
{
    class InterviewerDB : MySqlExtension<Interviewer>
    {
        /*private Database db = new Database();
        List<IInterviewer> _listin;
        Interviewer _in;*/
        public void CreateInterviewer(Interviewer interviewer)
        {
            string query = "INSERT INTO UCL_Interviewer(Id, Firstname, Lastname) VALUES(@Id, @Firstname, @Lastname)";
            string param1 = "@Id";
            string param2 = "@Firstname";
            string param3 = "@Lastname";

            List<string> tempP = new List<string>
            {
                param1, param2, param3
            };

            List<object> tempV = new List<object>
            {
                interviewer.Id, interviewer.Firstname, interviewer.Lastname
            };
            ExecuteCmd(query, SetParametersList(tempP, tempV));
        }

        public Interviewer GetInterviewer(string id)
        {
            string query = "SELECT * FROM UCL_Interviewer WHERE UCL_Interviewer.Id = @Id";
            string param1 = "@Id";
            List<object[]> temp = ExecuteReaderList(query, id);
            foreach (object[] item in temp)
            {
                if (item != null)
                {
                    return new Interviewer() { Id = item[0].ToString(), Firstname = item[1].ToString(), Lastname = item[2].ToString() };
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        public List<Interviewer> GetAllInterviewers()
        {
            string query = "SELECT * FROM UCL_Interviewer";
            List<object[]> temp = ExecuteReaderList(query, new Interviewer());
            List<Interviewer> list = new List<Interviewer>();
            foreach (object[] item in temp)
            {
                if (item != null)
                {
                    list.Add(
                    new Interviewer
                    {
                        Id = item[0].ToString(),
                        Firstname = item[1].ToString(),
                        Lastname = item[2].ToString()
                    }
                    );
                }
            }
            return list;
        }
    }
}
