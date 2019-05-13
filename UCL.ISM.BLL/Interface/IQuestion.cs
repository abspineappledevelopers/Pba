using System;
using System.Collections.Generic;
using System.Text;

namespace UCL.ISM.BLL.Interface
{
    public interface IQuestion
    {
        int Id { get; set; }
        string Quest { get; set; }
        string Answer { get; set; }
    }
}
