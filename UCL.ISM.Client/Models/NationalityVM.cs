using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UCL.ISM.BLL.BLL;

namespace UCL.ISM.Client.Models
{
    public class NationalityVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsEU { get; set; }

        public static implicit operator NationalityVM(Nationality nationality)
        {
            return new NationalityVM
            {
                Id = nationality.Id,
                Name = nationality.Name,
                IsEU = nationality.IsEU
            };
        }

        public static implicit operator Nationality(NationalityVM vm)
        {
            return new Nationality
            {
                Id = vm.Id,
                Name = vm.Name,
                IsEU = vm.IsEU
            };
        }
    }
}
