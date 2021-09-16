using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GETMETHOD_with_HTTPCLIENT
{
    class Program
    {
        

        static void Main(string[] args)
        {
            Consume_WebApi().Wait();
        }

        static async Task Consume_WebApi()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:57489/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("api/employee/list");
            if(response.IsSuccessStatusCode)
            {
                dynamic result = await response.Content.ReadAsStringAsync();
                RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(result);

                foreach(var item in rootObject.Employee)
                {
                    Console.WriteLine("{0}\t${1}\t{2}", item.EmployeeID, item.Email, item.Address, item.JoiningDate);
                }

                Console.ReadKey();
            }

        }

        public class RootObject
        {
            public List<Employee> Employee { get; set; }
        }

        public class Employee
        {
            public int EmployeeID { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
            public DateTime JoiningDate { get; set; }
        }
    }
}
