using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using UCL.ISM.BLL.Interface;
using UCL.ISM.BLL.BLL;
using UCL.ISM.BLL.DAL;

namespace UCL.ISM.BLL.DAL
{
    class InterviewerDB : MySqlExtension<Interviewer>
    {
        private Database db = new Database();
        List<Interviewer> _listin;
        Interviewer _in;
        

        public List<Interviewer> GetAllInterviewers()
        {
            string query = "SELECT * FROM UCL_Interviewer";
            return ExecuteReaderList(query, new Interviewer());
        }
    }
}
