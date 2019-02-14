using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MasGlobalTestApi.DataAccessHandler.Api
{
    public class DataAccessLayer
    {
        public string SerializedResponse = "";

        public DataAccessLayer()
        {
        }

        public string ResponseApi()
        {
            return this.SerializedResponse;
        }

        public async Task CallWebAPIAsync(Uri uri, string method)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = uri;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync(method);
                    if (response.IsSuccessStatusCode)
                    {
                        this.SerializedResponse = await response.Content.ReadAsStringAsync();
                        System.Diagnostics.Debug.WriteLine("SUCCESS");
                    }
                    else
                    {
                        this.SerializedResponse = response.StatusCode.ToString();
                        System.Diagnostics.Debug.WriteLine(response.StatusCode.ToString());
                    }
                }
                catch (HttpRequestException exception)
                {
                    System.Diagnostics.Debug.WriteLine("CAUGHT EXCEPTION:");
                    System.Diagnostics.Debug.WriteLine(exception);
                }

            }
        }
    }
}
