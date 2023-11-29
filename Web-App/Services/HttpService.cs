using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Web_App.Models;

namespace EmployeeManagement.Web.Services
{
    public class EmployeeService
    {
        private readonly HttpClient httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<User>> GetEmployees()
        {
            return await httpClient.GetFromJsonAsync<User[]>("api/user/all");
        }

    }   
}