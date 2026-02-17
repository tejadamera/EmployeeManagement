using EmployeeManagement.Data;
using EmployeeManagement.DTOs;
using EmployeeManagement.Models;
using EmployeeManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlTypes;

namespace EmployeeManagement.Controllers
{
    //<summary>
    //Manages employee data
    //</summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        //<summary>
        //Gets paginated list of active employees
        //</summary>
        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees([FromQuery] EmployeeQueryParams query)
        {
            var employees = await _employeeService.GetAllAsync(query);
            return Ok(employees);
        }
        [HttpGet("GetEmployeeById/{id:int}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null) return NotFound();
            return Ok(employee);
        }

        //<summary>
        //Creates new employee
        //</summary>

        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeDto dto)
        {
            var employee = await _employeeService.CreateAsync(dto);
            if(employee == null) return Conflict("An employee with this email already exists.");
            return Ok(employee);
        }

        [HttpPut("UpdateEmployee/{id:int:min(1)}")]
        public async Task<IActionResult> UdateEmployee(int id, UpdateEmployeeDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updated = await _employeeService.UpdateAsync(id, dto);
            return updated ? NoContent() : NotFound();
        }

        [Authorize(Roles ="Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var deleted = await _employeeService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }


    }
}
