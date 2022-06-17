using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMVCProjectEntities.Concrete
{
    public class CostType:BaseEntity
    {
        public CostType()
        {
            Costs = new HashSet<Cost>();
        }
        [Display(Name = "Harcama Türü")]
        public string CostName { get; set; }
        public ICollection<Cost> Costs { get; set; }
    }
}
