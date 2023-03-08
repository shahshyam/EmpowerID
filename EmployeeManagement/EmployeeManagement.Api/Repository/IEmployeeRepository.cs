using EmployeeManagement.Api.Models;

namespace EmployeeManagement.Api.Repository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployeeAsync();
        Task<Employee> CreateEmployeeAsync(Employee employee);
        Task<Employee> UpdateEmployeeAsync(int Id, Employee employee);
        Task<Employee> DeleteEmployeeAsync(int Id);
        Task<Employee> GetEmployeeAsync(int Id);
    }
}
