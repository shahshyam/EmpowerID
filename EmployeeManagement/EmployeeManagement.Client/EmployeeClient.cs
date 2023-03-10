using EmployeeManagement.Client.RequestDto;
using EmployeeManagement.Client.Responses;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Net;

namespace EmployeeManagement.Client
{
    public sealed class EmployeeClient
    {
        private static readonly EmployeeClient instance = new EmployeeClient(); 
        private HttpClient _httpClient;
        static EmployeeClient()
        {
           
        }
        private EmployeeClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7164/api/");
        }

        public static EmployeeClient Instance
        {
            get
            {
                return instance;
            }
        }
        public async Task<List<Employee>> GetEmployees()
        {
            var employees = new List<Employee>();
            var response = await _httpClient.GetAsync("Employee");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string content = await response.Content.ReadAsStringAsync();
                employees = JsonConvert.DeserializeObject<List<Employee>>(content);
            }
            return employees;
        }
        public void CreateEmployee(EmployeeDto employee)
        {
            //var response = _httpClient.PostAsync("/employees");
        }
        public async Task<bool> DeleteEmployee(int Id)
        {
            var response = await _httpClient.DeleteAsync($"Employee/{Id}");
            if (response.StatusCode == HttpStatusCode.OK)
                return true;
            return false;
        }

        public void UpdateEmployee(int id, EmployeeDto employee)
        {

        }
    }
}