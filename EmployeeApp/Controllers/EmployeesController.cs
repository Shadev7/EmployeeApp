using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeApp.Models;
using Newtonsoft.Json;

namespace EmployeeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeContext _context;

        public EmployeesController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: api/Employees
       /*[HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }
       */
        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeID(int id)
        {
            //return NotFound("Here I am");
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound($"Employee with Id {id} does not exit in the database");
            }

            return employee;
        }
        
        // PUT: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateEmployeeIDS([FromBody] Employee employee)
        {
            /*
            if (id != employee.EmployeeID)
            {
                return BadRequest($"Employee with Id {id} does not exit in the database");
            }
            */
            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(employee.EmployeeID))
                {
                    return NotFound("Employee does  not exist");
                }
                 
                else
                {
                   throw;
                }
            }
            catch (Exception)
            {
                if (!DepartmentExists(employee.DepartmentId))
                {
                    return NotFound("Department does  not exist");
                }
                else if (!JobExists(employee.JobId))
                {
                    return NotFound("Job does  not exist");
                }
                else
                {
                    throw;
                }
            }

            //return NoContent();
            return Ok($"Employee with ID {employee.EmployeeID} has been updated");
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> AddNewEmployee(Employee employee)
        {
            if (!EmployeeExists(employee.EmployeeID))
            {
                var department = await _context.Departments.FindAsync(employee.DepartmentId);
                var job = await _context.Jobs.FindAsync(employee.JobId);
                var manager = await _context.Employees.FindAsync(employee.ManagerId);

                //Check for valid foreign key
                if (department == null || job == null || manager == null)
                {
                    return BadRequest("The ManagerID, DeparmentID or JobId for the employee is invalid");
                }
                else
                {
                    _context.Employees.Add(employee);
                    await _context.SaveChangesAsync();

                    //return CreatedAtAction("GetEmployee", new { id = employee.EmployeeID }, employee);
                    return CreatedAtAction(nameof(GetEmployeeID), new { id = employee.EmployeeID }, employee);
                    //return CreatedAtAction(nameof(GetEmployees), new { id = employee.EmployeeID }, employee);
                }
            }
            return BadRequest("Employee already exists");

        }

        // DELETE: api/Employees/5
        //[HttpDelete("{id}")]
        [HttpDelete]
        public async Task<IActionResult> RemoveEmployeeByID([FromQuery] int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return Ok("Employee deleted");
        }
 
        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeID == id);
        }

        private bool DepartmentExists (int id)
        {
            return _context.Departments.Any(d => d.DepartmentId == id);

        }
        private bool JobExists(int id)
        {
            return _context.Jobs.Any(j => j.JobId == id);

        }



        [HttpGet]
        public async Task<ActionResult<Object>> GetEmployees([FromQuery] int id, [FromQuery] int v)
        {

            // Return all employees
            //return Ok(id);
            if (String.IsNullOrEmpty(id.ToString()) || id==0)
            {
                //return Ok("In All");
                return await _context.Employees.ToListAsync();
            }

            
            else
            {
                //Return employee by ID 
                //Usage : https://localhost:44386/api/Employees?id={id}
                if (String.IsNullOrEmpty(v.ToString()) || v == 0)
                {
                    var employee = await _context.Employees.FindAsync(id);

                    if (employee == null)
                    {
                        return NotFound($"Employee with Id {id} does not exit in the database");
                    }

                    return employee;
                }

                //Return full employee details
                // Usage : https://localhost:44386/api/Employees?id={id}&v=1
                else if (v == 1)
                {
                    var employee = await _context.Employees.FindAsync(id);
                    //return employee;
                    //return Ok(employee.DepartmentId);
                    if (!String.IsNullOrEmpty(employee.DepartmentId.ToString()))
                    {

                        var department = await _context.Departments.FindAsync(employee.DepartmentId);
                        var job = await _context.Jobs.FindAsync(employee.JobId);
                        var manager = await _context.Employees.FindAsync(employee.ManagerId);
                        EmpDBModel empdb = new EmpDBModel();
                      

                        empdb.EmployeeID = id;
                        empdb.FirstName = employee.FirstName;
                        empdb.LastName = employee.LastName;
                        empdb.Email = employee.Email;
                        empdb.Phone = employee.Phone;
                        empdb.JobId = employee.JobId;
                        empdb.DepartmentId = employee.DepartmentId;
                        empdb.ManagerId = employee.ManagerId;
                        empdb.DepartmentName = department.DepartmentName;
                        empdb.JobTitle = job.JobTitle;
                        empdb.ManagerName = manager.FirstName + " " + manager.LastName;
                        empdb.ManagerEmail = manager.Email;

                        return empdb;
                    }
                    return NotFound("Employee not found");
                }
                //Return full employee details
                // Usage : https://localhost:44386/api/Employees?id={id}&v=2
                else
                {
                    

                        var q2 =
                            from emp in _context.Employees.Where(o => o.EmployeeID == id)
                            join dept in _context.Departments
                            on emp.DepartmentId equals dept.DepartmentId
                            join jb in _context.Jobs
                            on emp.JobId equals jb.JobId
                            select new { Employee = emp.FirstName, Department = dept, Job  = jb };


                        return q2.ToList();
                    }
            }
           
        }
    }
}
