﻿using System;
using System.Collections.Generic;
using System.Text;
using UCL.ISM.BLL.Interface;
using UCL.ISM.BLL.DAL;

namespace UCL.ISM.BLL.BLL
{
    public class Question : IQuestion
    {
        public int Id { get; set; }
        public string Content { get; set; }
    }
}
