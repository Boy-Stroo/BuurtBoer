using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Web_App.Data;

namespace EmployeeManagement.Web.Services
{
    public class EmployeeService
    {
        private readonly HttpClient httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<Users>> GetEmployees()
        {
            return await httpClient.GetFromJsonAsync<Users[]>("api/User/All");
        }

    }   
}