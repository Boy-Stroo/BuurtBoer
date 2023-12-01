using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mobile_App
{
    // This is the service that will be used to make calls to the backend
    public class HTTPService
    {
        protected HttpClient _client;
        protected JsonSerializerOptions _serializerOptions;
        // Default gateway for android emulator: 10.0.2.2 (the ip address through which the android emulator can connect to the internet) 
        // Translates to our local pc then port 5077 where backend is running
        protected string _domain = "http://10.0.2.2:5077";

        public HTTPService()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }
    }
}
