using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using UserOperationsPOC.DataTransferObjects;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace UserOperationsPOC
{
    public static class UserOperationsPOC
    {
        [FunctionName("CreateUser")]
        public static async Task<IActionResult> CreateUser(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "user")]HttpRequestMessage req, ILogger log)
        {
            log.LogInformation("Creating a new User");
            try
            {
                var content = req.Content;
                
                using (var client = new HttpClient())
                {
                    var responseData = await client.PostAsync("https://reqres.in/api/users", content);
                    return new OkObjectResult(responseData.IsSuccessStatusCode);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [FunctionName("GetUserById")]
        public static async Task<IActionResult> GetUser(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "user/{id}")]HttpRequestMessage req, ILogger log, string id)
        {
            log.LogInformation("Getting user by id");
            try
            {
                var content = req.Content;
                string jsonContent = content.ReadAsStringAsync().Result;
                dynamic requestPram = JsonConvert.DeserializeObject<UserDTO>(jsonContent);

                if (requestPram == null)
                {
                    return new NotFoundResult();
                }

                using (var client = new HttpClient())
                {
                    var responseData = await client.GetStringAsync(string.Format("https://reqres.in/api/users/" + id));
                    return new OkObjectResult(responseData);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [FunctionName("GetAllUsers")]
        public static async Task<IActionResult> GetAllUsers(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "user")]HttpRequestMessage req, ILogger log)
        {
            log.LogInformation("Getting users");
            try
            {
                var content = req.Content;
                string jsonContent = content.ReadAsStringAsync().Result;
                dynamic requestPram = JsonConvert.DeserializeObject<UserDTO>(jsonContent);

                if (requestPram == null)
                {
                    return new NotFoundResult();
                }

                using (var client = new HttpClient())
                {
                    var responseData = await client.GetStringAsync(string.Format("https://reqres.in/api/users/"));
                    return new OkObjectResult(responseData);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [FunctionName("UpdateUser")]
        public static async Task<IActionResult> UpdateUser(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "user/{id}")]HttpRequestMessage req, ILogger log, string id)
        {
            log.LogInformation("Updating user by id");
            try
            {
                var content = req.Content;
                string jsonContent = content.ReadAsStringAsync().Result;
                dynamic requestPram = JsonConvert.DeserializeObject<UserDTO>(jsonContent);

                if (requestPram == null)
                {
                    return new NotFoundResult();
                }

                using (var client = new HttpClient())
                {
                    var responseData = await client.PutAsync(string.Format("https://reqres.in/api/users/" + id), content);
                    return new OkObjectResult(responseData.IsSuccessStatusCode);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
