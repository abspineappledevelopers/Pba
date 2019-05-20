using System.Collections.Generic;
using UCL.ISM.BLL.BLL;

namespace UCL.ISM.BLL.DAL
{
    class InterviewerDB : MySqlExtension<Interviewer>
    {
        /*private Database db = new Database();
        List<IInterviewer> _listin;
        Interviewer _in;*/


        public List<Interviewer> GetAllInterviewers()
        {
            string query = "SELECT * FROM UCL_Interviewer";
            object[] temp = ExecuteReaderList(query, new Interviewer());
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
