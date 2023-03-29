using Newtonsoft.Json;
using System.Text;

namespace HoleInOneControl.Functions
{
    public static class APIServices
    {

        private static int timeout = 30;
        private static string baseurl = "https://localhost/HoleInOneControlAPI/";

        public static async System.Threading.Tasks.Task<IEnumerable<HoleInOneControlModel.User>> UsersGetList()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };


            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
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


        
        public static async System.Threading.Tasks.Task UserSet(HoleInOneControlModel.User object_to_serialize)
        {

            var json_ = Newtonsoft.Json.JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };


            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
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
        



        public static async System.Threading.Tasks.Task<IEnumerable<HoleInOneControlModel.Article>> ArticlesGetList()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };


            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
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


        public static async System.Threading.Tasks.Task ArticleSet(HoleInOneControlModel.Article object_to_serialize)
        {

            var json_ = Newtonsoft.Json.JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };


            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);

            var response = await httpClient.PostAsync(baseurl + "Articles/CreateArticle", content);

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


    }
}
