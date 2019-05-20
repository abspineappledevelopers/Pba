using System.Collections.Generic;

namespace UCL.ISM.BLL.Interface
{
    public interface INationality
    {
        int Id { get; set; }
        string Name { get; set; }
        bool IsEU { get; set; }

        bool IsEu(int id);
        List<INationality> GetAllNationalities();
    }
}
