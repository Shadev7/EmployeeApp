using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace EmployeeApp.Models
{
    public class EmpDBModel :Employee
    {
        //public Employee emp = new Employee();
        //public Department dept = new Department();
        
        private string departmentName;
        private string jobTitle;
        private string managerName;
        private string managerEmail;

        [Required]
        public string DepartmentName { get => departmentName; set => departmentName = value; }

        [Required]
        public string JobTitle { get => jobTitle; set => jobTitle = value; }
        public string ManagerName { get => managerName; set => managerName = value; }
        public string ManagerEmail { get => managerEmail; set => managerEmail = value; }
    }
}
