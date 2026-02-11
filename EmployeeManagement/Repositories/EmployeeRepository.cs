using EmployeeManagement.Data;
using EmployeeManagement.DTOs;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;
        public EmployeeRepository(AppDbContext context)
        {
                _context = context;
        }

        public async Task<List<Employee>> GetAllAsync(EmployeeQueryParams query)
        {
            var employees = _context.Employees.Where(e => e.IsActive).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Search))
                employees = employees.Where(e=>e.Name.Contains(query.Search));
            if (!string.IsNullOrEmpty(query.Department))
                employees = employees.Where(e=>e.Department == query.Department);
            return await employees.Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();

            //return await _context.Employees.Where(e => e.IsActive).ToListAsync();
        }

        public async Task<Employee?> GetbyIdAsync(int id)
        {
            return await _context.Employees.FirstOrDefaultAsync(e=>e.Id==id && e.IsActive);
        }

        public async Task AddAsync(Employee employee)
        {
           await _context.Employees.AddAsync(employee);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
