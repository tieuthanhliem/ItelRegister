using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ItelClient
{
    internal class LivenessRequest
    {
        protected string _serviceUrl = "https://api.idg.vnpt.vn/ai/v2/card/liveness?challenge_code=11111";
        protected JObject _requestBody;
        public JObject _responseBody;

        HttpClient _client;
        string tokenId;

        MultipartFormDataContent _form;

        public LivenessRequest(string imgHash, string token, string tokenId)
        {
            var handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
            };

            _client = new HttpClient(handler);

            //_client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            _client.DefaultRequestHeaders.Accept.ParseAdd("*/*");
            _client.DefaultRequestHeaders.UserAgent.ParseAdd("CCBS/1 CFNetwork/1240.0.4 Darwin/20.6.0");
            _client.DefaultRequestHeaders.AcceptLanguage.TryParseAdd("vi-vn");

            _client.DefaultRequestHeaders.Add("Token-id", tokenId);
            _client.DefaultRequestHeaders.Add("mac-address", "IOS-12-10EED676-7E17-49B3-A980-7971717FA2F6");
            _client.DefaultRequestHeaders.Add("Token-key", "MFwwDQYJKoZIhvcNAQEBBQADSwAwSAJBAN7osWqbjgvVl9V1323Vm3qaW30GUdEmako+pDXhs6HuW6gNw39lCMepLXCAGw+zrl/L8HqvE+j06nyvVXpioH0CAwEAAQ==");


            _client.DefaultRequestHeaders.Authorization
                = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI0N2M5YTU3Mi03Njg0LTExZWItOGUyZi0yNTQ5ZDNmNGY0YzIiLCJhdWQiOlsicmVzdHNlcnZpY2UiXSwidXNlcl9uYW1lIjoicWxzcGljQGlkZy52bnB0LnZuIiwic2NvcGUiOlsicmVhZCJdLCJpc3MiOiJodHRwczovL2xvY2FsaG9zdCIsIm5hbWUiOiJxbHNwaWNAaWRnLnZucHQudm4iLCJ1dWlkX2FjY291bnQiOiI0N2M5YTU3Mi03Njg0LTExZWItOGUyZi0yNTQ5ZDNmNGY0YzIiLCJhdXRob3JpdGllcyI6WyJVU0VSIl0sImp0aSI6IjRjMDg3ODA4LWMyZTAtNDk5Ni1hOWI3LWE2YTdhZmJjM2RlZCIsImNsaWVudF9pZCI6ImFkbWluYXBwIn0.Njnp2eTPEJjtQeMcstgR2Q2dZomuuFulwX_dXH5MTM-yVlu7mvu2bNHWNlwKmhr7-oa0ZEik8fdIhqPV9RmzRGZygJevKJq6czdYw576jKbNbiwv1JPebjOPqPYb_j1P-nknA7nmQJFKtaWIgYBRw3SezAHe-dlZW413bQ7rOJoLDmAYXtZ-9wqCUxj7Ls4Q8vbTn39sNpKaC5NgpDbR65nut2jGMFnyA_HQ_w3W-bH4DoW_NhvwRYJFScsLSPGzz641BVvffKhHQt-i40R-kyL5hVhmdwUxrH4Ih0wSC_JLfpFoGJAxaMqRCplqpGUoYNbOqCjsrcVHdAndkuBWOg");

            _requestBody = JObject.FromObject(new {
                client_session = "IOS_iphone11_ios14.8.1_Device_1.0.2_10EED676-7E17-49B3-A980-7971717FA2F6_1641628083",
                type = -1,
                crop_param = "0.18,0.53",
                token,
                img = imgHash,
            });
        }

        public string GetDisplayMessage()
        {
            return _responseBody["status"]["displayMessage"].ToString();
        }

        public string GetCodeStatus()
        {
            return _responseBody["status"]["code"].ToString();
        }

        public bool CreateRequest(bool logRes = true)
        {
            try
            {
                var content = new StringContent(_requestBody.ToString(), Encoding.UTF8, "application/json");
                string res = _client.PostAsync(_serviceUrl, content).Result.Content.ReadAsStringAsync().Result;

                _responseBody = JObject.Parse(res);

                return true;
            }
            catch (Exception ex)
            {
            }
            return false;
        }
    }
}
