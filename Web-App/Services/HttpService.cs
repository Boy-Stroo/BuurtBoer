using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Web_App
{
    // This is the service that will be used to make calls to the backend
    public class HTTPService
    {
        protected HttpClient _client;
        protected string _domain = "http://localhost:5077";

        public HTTPService()
        {
            _client = new HttpClient();
        }
    }
}
