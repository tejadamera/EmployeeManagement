using EmployeeManagement.DTOs;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Services
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllAsync(EmployeeQueryParams query);
        Task<Employee?> GetByIdAsync(int id);
        Task<Employee> CreateAsync(CreateEmployeeDto dto);
        Task<bool> UpdateAsync(int id, UpdateEmployeeDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
