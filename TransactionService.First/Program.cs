using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace TransactionService.First
{
    class Program
    {
        static async Task Main(string[] args)
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8081/");
            listener.Start();

            while (true)
            {
                var context = await listener.GetContextAsync();

                var correlationId = context.Request.Headers["X-CorrelationId"];

                HttpRequestMessage message = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("http://localhost:8082/")
                };

                message.Headers.Add("X-CorrelationId", correlationId);

                using (HttpClient client = new HttpClient())
                {
                    var httpresponse = await client.SendAsync(message);
                }
                Console.Read();
            }
        }
    }
}
