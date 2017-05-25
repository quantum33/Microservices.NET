using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Microservices.Common
{
    public static class HttpUtility
    {
        public static string HttpGet(string uri)
            => HttpGetAsync(new Uri(uri)).Result;

        public static async Task<string> HttpGetAsync(string uri)
            => await HttpGetAsync(new Uri(uri));

        public static async Task<string> HttpGetAsync(Uri uri)
        {
            using (var http = new HttpClient { BaseAddress = uri })
            {
                var res = await http.GetStringAsync("");
                return res;
            }
        }
    }
}
