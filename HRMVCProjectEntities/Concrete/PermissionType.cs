using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMVCProjectEntities.Concrete
{
    public class PermissionType : BaseEntity
    {
		public PermissionType()
		{
            Permissions = new HashSet<Permission>();
        }
        [Display(Name = "İzin Türü")]
        public string PermissionName { get; set; }
        [Display(Name = "İzinli Gün Sayısı")]
        public int AllowedDays { get; set; }
        [Display(Name = "İzinler")]
        public ICollection<Permission> Permissions { get; set; }

    }
}
