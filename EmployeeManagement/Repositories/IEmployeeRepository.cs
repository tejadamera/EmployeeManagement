using EmployeeManagement.DTOs;
using EmployeeManagement.Models;

namespace EmployeeManagement.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllAsync(EmployeeQueryParams query);
        Task<Employee?> GetbyIdAsync(int id);
        Task AddAsync(Employee employee);
        Task SaveChangesAsync();
    }
}
