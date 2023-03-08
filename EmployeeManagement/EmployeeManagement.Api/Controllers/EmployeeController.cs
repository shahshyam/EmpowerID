using AutoMapper;
using EmployeeManagement.Api.Dtos;
using EmployeeManagement.Api.Models;
using EmployeeManagement.Api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public EmployeesController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        // GET: api/Employees/
        [HttpGet]
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _employeeRepository.GetAllEmployeeAsync();
        }
        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee([FromRoute] int id)
        {
            var employee = await _employeeRepository.GetEmployeeAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        // POST: api/Employees
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto employeeDto)
        {
            if (employeeDto == null)
            {
                return BadRequest("Employee can not be null");
            }
            var employee = _mapper.Map<Employee>(employeeDto);
            if(!ModelState.IsValid)
            {
                return BadRequest("Invalid data is provided");
            }
            await _employeeRepository.CreateEmployeeAsync(employee);
            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
        {
            var employee = await _employeeRepository.DeleteEmployeeAsync(id);
            if (employee == null)
            {
                return NotFound($"Employee does not exist with Id:{id}");
            }
            return Ok(employee);
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee([FromRoute] int id, [FromBody] EmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            var updatedEmployee = await _employeeRepository.UpdateEmployeeAsync(id, employee);
            if (updatedEmployee == null)
            {
                return NotFound("Unable to update employee");
            }
            return Ok(updatedEmployee);
        }
    }

}