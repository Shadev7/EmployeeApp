using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace EmployeeApp.Models
{
    public class Department
    {
        private int departmentId;
        private string departmentName;

        [Required]
        public int DepartmentId { get => departmentId; set => departmentId = value; }
        [Required]
        public string DepartmentName { get => departmentName; set => departmentName = value; }
    }
}
