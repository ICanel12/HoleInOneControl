using HoleInOneControl.Controllers;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.JSInterop;

namespace HoleInOneControl.Functions
{
    public static class APIServices
    {

        private static int timeout = 30;
        private static string baseurl = "https://localhost:7099/";


        public static async System.Threading.Tasks.Task<HoleInOneControlModel.Token> LoginAPILogin(HoleInOneControlModel.Token object_to_serialize)
        {
            var json_ = Newtonsoft.Json.JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);

            var response = await httpClient.PostAsync(baseurl + "LoginAPI/Login", content);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<HoleInOneControlModel.Token>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }

        }

        public static async System.Threading.Tasks.Task<IEnumerable<HoleInOneControlModel.User>> UsersGetList(string accessToken)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };


            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);
            var response = await httpClient.GetAsync(baseurl + "Users/GetUsers");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IEnumerable<HoleInOneControlModel.User>>(await response.Content.ReadAsStringAsync());
            }

            else
            {
                throw new Exception(response.StatusCode.ToString());

            }
        }


        
        public static async System.Threading.Tasks.Task UserSet(HoleInOneControlModel.User object_to_serialize, string accessToken)
        {
            EncryptMD5 encr = new EncryptMD5();
            object_to_serialize.Password = encr.Encrypt(object_to_serialize.Password);
            var json_ = Newtonsoft.Json.JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };


            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);

            var response = await httpClient.PostAsync(baseurl + "Users/CreateUser", content);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }


        public static async System.Threading.Tasks.Task UpdateUser(HoleInOneControlModel.User object_to_serialize)
        {

            var json_ = Newtonsoft.Json.JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };


            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);

            var response = await httpClient.PutAsync(baseurl + $"Users/UpdateUser/{object_to_serialize.IdUser}", content);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(response.StatusCode.ToString());
            }

        }



        public static async Task DeleteUser(int idUser)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);

            var response = await httpClient.DeleteAsync(baseurl + $"Users/DeleteUser/{idUser}");

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        



        public static async System.Threading.Tasks.Task<IEnumerable<HoleInOneControlModel.Article>> ArticlesGetList(string accessToken)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };


            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);

            var response = await httpClient.GetAsync(baseurl + "Articles/GetArticles");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IEnumerable<HoleInOneControlModel.Article>>(await response.Content.ReadAsStringAsync());
            }

            else
            {
                throw new Exception(response.StatusCode.ToString());

            }
        }


        public static async System.Threading.Tasks.Task ArticleSet(HoleInOneControlModel.Article object_to_serialize, string accessToken)
        {

            var json_ = Newtonsoft.Json.JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };


            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);

            var response = await httpClient.PostAsync(baseurl + "Articles/CreateArticle", content);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(response.StatusCode.ToString());
            }

        }

        public static async System.Threading.Tasks.Task TransactionSet(HoleInOneControlModel.Article object_to_serialize, string accessToken)
        {
            var json_ = Newtonsoft.Json.JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };


            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);

            var response = await httpClient.PostAsync(baseurl + "Articles/CreateTransaction", content);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(response.StatusCode.ToString());
            }

        }



        public static async System.Threading.Tasks.Task UpdateArticle(HoleInOneControlModel.Article object_to_serialize)
        {

            var json_ = Newtonsoft.Json.JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };


            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);

            var response = await httpClient.PutAsync(baseurl + $"Article/UpdateArticle/{object_to_serialize.IdArticle}", content);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(response.StatusCode.ToString());
            }

        }



        public static async Task DeleteArticle(int idArticle)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);

            var response = await httpClient.DeleteAsync(baseurl + $"Users/DeleteArticle/{idArticle}");

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }



		//HANDICAP

		public static async System.Threading.Tasks.Task<IEnumerable<HoleInOneControlModel.Handicap>> HandicapList(string accessToken)
		{
			HttpClientHandler clientHandler = new HttpClientHandler();
			clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };


			// Pass the handler to httpclient(from you are calling api)
			HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);

			var response = await httpClient.GetAsync(baseurl + "Handicaps/GetPartidas");
            
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return JsonConvert.DeserializeObject<IEnumerable<HoleInOneControlModel.Handicap>>(await response.Content.ReadAsStringAsync());
			}

			else
			{
				throw new Exception(response.StatusCode.ToString());

			}
		}



        public static async System.Threading.Tasks.Task CreateHandicap(HoleInOneControlModel.Handicap object_to_serialize, string accessToken)
        {
            object_to_serialize.DateHour = DateTime.Now;

            var json_ = Newtonsoft.Json.JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };


            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);

            var response = await httpClient.PostAsync(baseurl + "Handicaps/CreatePartida", content);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(response.StatusCode.ToString());
            }

        }


    }
}
