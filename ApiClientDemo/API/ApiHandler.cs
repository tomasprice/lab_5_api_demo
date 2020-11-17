using restApiDemo.Data.Models;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiClientDemo.API
{
    public class ApiHandler
    {
        private readonly RestClient _client;

        public ApiHandler(string address)
        {
            this._client = new RestClient(address);
        }

        public List<T> Get<T>()
        {
            var request = new RestRequest("get", Method.GET);
            var result = _client.Execute<List<T>>(request).Data;

            return result;
        }

        public bool Add<T>(T item)
        {
            var request = new RestRequest("add", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddJsonBody(item);

            var response = _client.Execute(request);

            return response.IsSuccessful;
        }

        public bool Update<T>(T item, int id)
        {
            var request = new RestRequest($"update/{id}", Method.PUT)
            {
                RequestFormat = DataFormat.Json
            };

            request.AddJsonBody(item);

            var response = _client.Execute(request);

            return response.IsSuccessful;
        }

        public bool Delete(int id)
        {
            var request = new RestRequest($"delete/{id}", Method.DELETE);
            var result = _client.Execute(request);

            return result.IsSuccessful;
        }


        public T GetPerson<T>(int id)
        {
            var request = new RestRequest($"get/{id}", Method.GET);
            var result = _client.Execute<T>(request).Data;

            return result;
        }
    }
}
