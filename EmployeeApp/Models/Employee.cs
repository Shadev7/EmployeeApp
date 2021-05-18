using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EmployeeApp.Models
{
    public class Employee
    {
        private int employeeID;
        private string firstName;
        private string lastName;
        private string email;
        private string phone;
        private int jobId;
        private int managerId;
        private int departmentId;
        /*private struct EmployeeManager {
            private EmployeeManager(string mEmail, string mName)
            {

            }
        };*/
        

        [Required]
        public int EmployeeID { get => employeeID; set => employeeID = value; }
        [Required]
        public string FirstName { get => firstName; set => firstName = value; }

        [Required]
        public string LastName { get => lastName; set => lastName = value; }

        [Required][EmailAddress]
        public string Email { get => email; set => email = value; }
        [Phone(ErrorMessage ="Please enter a valid phone number")]
        public string Phone { get => phone; set => phone = value; }
        public int JobId { get => jobId; set => jobId = value; }
        public int ManagerId { get => managerId; set => managerId = value; }
        public int DepartmentId { get => departmentId; set => departmentId = value; }
        

    }
}
