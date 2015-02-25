namespace NServiceBus.ScriptCs.ServiceControl
{
    using System;
    using System.Collections.Generic;
    using RestSharp;

    public class Client
    {
        private readonly RestClient client;

        public Client(Uri baseUri)
        {
            this.client = new RestClient(baseUri);
        }

        public IList<Endpoint> GetEndpoints()
        {
            return this.Get<List<Endpoint>>("endpoints");
        }

        public IList<Error> GetErrors()
        {
            return this.Get<List<Error>>("errors");
        }

        public T Get<T>(string resource) where T : class, new()
        {
            Console.Out.WriteLine("GET /" + resource);

            var response = this.client.Execute<T>(new RestRequest(resource, Method.GET));
            if (response.ResponseStatus == ResponseStatus.Error)
            {
                throw new InvalidOperationException(response.ErrorMessage);
            }

            return response.Data;
        }
    }
}
