using AutoMapper;
using EmployeeManagement.Data;
using EmployeeManagement.DTOs;
using EmployeeManagement.Models;
using EmployeeManagement.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;
        public EmployeeService(IEmployeeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<Employee>> GetAllAsync(EmployeeQueryParams query)
        {
            return await _repository.GetAllAsync(query);
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _repository.GetbyIdAsync(id);
        }

        public async Task<Employee> CreateAsync(CreateEmployeeDto dto)
        {
            var employee = _mapper.Map<Employee>(dto);
            employee.IsActive = true;
            await _repository.AddAsync(employee);
            await _repository.SaveChangesAsync();
            return employee;
        }

        public async Task<bool> UpdateAsync(int id, UpdateEmployeeDto dto)
        {
            var employee = await _repository.GetbyIdAsync(id);
            if (employee == null || !employee.IsActive) return false;
            _mapper.Map(dto,employee);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            //var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            var employee = await _repository.GetbyIdAsync(id);
            if (employee == null || !employee.IsActive) return false;
            employee.IsActive = false;
            await _repository.SaveChangesAsync();
            return true;
        }

    }
}
