using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Api.Gateway
{
    class Program
    {
        static async Task Main(string[] args)
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8080/");
            listener.Start();

            while (true)
            {
                var context = await listener.GetContextAsync();

                
                HttpListenerRequest request = context.Request;

                HttpListenerResponse response = context.Response;

                HttpRequestMessage message = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("http://localhost:8081/")
                };

                var corrId = Guid.NewGuid().ToString();

                message.Headers.Add("X-CorrelationId", corrId);

                using (HttpClient client = new HttpClient())
                {
                    var httpresponse = await client.SendAsync(message);
                }
                Console.Read();
            }
        }
    }
}
