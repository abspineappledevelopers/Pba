using System;
using System.Collections.Generic;
using System.Text;
using UCL.ISM.BLL.DAL;
using UCL.ISM.BLL.Interface;

namespace UCL.ISM.BLL.BLL
{
    public class Nationality : INationality
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsEU { get; set; }

        private NationalityDB _db;

        public List<Nationality> GetAllNationalities()
        {
            _db = new NationalityDB();

            return _db.GetAllNationalities();
        }
    }
}
