using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;

namespace Emaillabs_sendmail_example
{
    class EmaillabsResponse
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// ! Important: second string in dictionary can store returned message-id OR error with following email !
        /// </summary>
        [JsonProperty("data")]
        public List<Dictionary<string,string>> Data { get; set; }

        [JsonProperty("req_id")]
        public string ReqId { get; set; }
    }    

    class Program
    {
        static void Main(string[] args)
        {

            RestClient client = new RestClient("https://api.emaillabs.net.pl/api/new_sendmail")
            {
                Authenticator = new HttpBasicAuthenticator("apikey", "secretkey")
            };

            RestRequest request = new RestRequest(Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            request.AddParameter("smtp_account", "number.smtp_name.smtp");
            request.AddParameter("subject", "Subject");
            request.AddParameter("html", "Html message");
            request.AddParameter("txt", "Text version of message");
            request.AddParameter("from", "from@example.com");
            request.AddParameter("to[test@example.com]", "message-id");

            EmaillabsResponse response = JsonConvert.DeserializeObject<EmaillabsResponse>(client.Execute(request).Content);

            Console.WriteLine("Emaillabs code: " + response.Code);
            Console.WriteLine("Emaillabs message: " + response.Message);
            Console.WriteLine("Emaillabs req_id: " + response.ReqId);

            Console.ReadLine();
        }
    }
}
