using System;
using System.Net;
using System.Threading.Tasks;

namespace TransactionService.Second
{
    class Program
    {
        static async Task Main(string[] args)
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8082/");
            listener.Start();

            while (true)
            {
                var context = await listener.GetContextAsync();
                context.Response.Headers.Add("X-CorrelationId", context.Request.Headers["X-CorrelationId"]);
            }
        }
    }
}
