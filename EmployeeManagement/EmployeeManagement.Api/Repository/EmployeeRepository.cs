using EmployeeManagement.Api.DbContexts;
using EmployeeManagement.Api.Models;

namespace EmployeeManagement.Api.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDBContext _dbContext;
        public EmployeeRepository(EmployeeDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            _dbContext.Employees.Add(employee);
            await _dbContext.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee> DeleteEmployeeAsync(int Id)
        {
            var employee = await GetEmployeeAsync(Id);
            if (employee == null)
            {
                return employee;
            }
            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();

            return employee;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeeAsync()
        {
            return _dbContext.Employees;
        }

        public async Task<Employee> GetEmployeeAsync(int Id)
        {
            Employee employee = await _dbContext.Employees.FindAsync(Id);
            return employee;
        }

        public async Task<Employee> UpdateEmployeeAsync(int Id, Employee employee)
        {
            _dbContext.Entry(employee).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.Entry(employee);
            await _dbContext.SaveChangesAsync();
            return employee;
        }
    }
}
