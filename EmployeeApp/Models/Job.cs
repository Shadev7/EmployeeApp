using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace EmployeeApp.Models
{
    public class Job
    {
        private int jobId;
        private string jobTitle;

        [Required]
        public int JobId { get => jobId; set => jobId = value; }
        [Required]
        public string JobTitle { get => jobTitle; set => jobTitle = value; }
    }
}
