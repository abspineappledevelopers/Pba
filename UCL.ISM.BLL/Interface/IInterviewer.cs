﻿using System;
using System.Collections.Generic;
using System.Text;
using UCL.ISM.BLL.BLL;

namespace UCL.ISM.BLL.Interface
{
    public interface IInterviewer
    {
        List<Interviewer> GetAllInterviewers();
    }
}
