using EmployeeManagement.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;

namespace EmployeeManagement.Api.DbContexts
{
    public class EmployeeDBContext:DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public EmployeeDBContext(DbContextOptions<EmployeeDBContext> options)
        : base(options)
        {

        }        
    }
}
