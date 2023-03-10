using EmployeeManagement.Client.RequestDto;
using EmployeeManagement.Client.Responses;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Net;
using System.Text;

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
        public async Task<List<Employee>> GetEmployeesASync()
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

        public async Task<Employee> CreateEmployeeAsync(EmployeeDto employee)
        {
            var jsonData = JsonConvert.SerializeObject(employee);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("Employee", content);
            if (response.StatusCode==HttpStatusCode.Created)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Employee>(responseContent);
            }
            return null;
        }

        public async Task<Employee> UpdateEmployeeAsync(int id, Employee employee)
        {
            var jsonData = JsonConvert.SerializeObject(employee);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"Employee/{id}", content);
            if (response.StatusCode==HttpStatusCode.OK)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Employee>(responseContent);
            }
            return null;
        }
        public async Task<bool> DeleteEmployee(int Id)
        {
            var response = await _httpClient.DeleteAsync($"Employee/{Id}");
            if (response.StatusCode == HttpStatusCode.NoContent)
                return true;
            return false;
        }
       
    }
}