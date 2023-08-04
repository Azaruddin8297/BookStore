using BookStore.Order.Entity;
using BookStore.Order.Interface;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace BookStore.Order.Services;

public class UserServices : IUserServices
{
    private readonly IConfiguration _config;
    public UserServices(IConfiguration config)
    {
            _config = config;
    }
    public async Task<UserEntity> GetUser(string jwtToken)
    {
        using(HttpClient client = new HttpClient())
        {
           client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
           HttpResponseMessage response = await client.GetAsync("https://localhost:7273/api/user/GetUser");
            if(response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                ResponseEntity apiResponse = JsonConvert.DeserializeObject<ResponseEntity>(responseContent);
                string apiStringResponse = apiResponse.Data.ToString();

                UserEntity user = JsonConvert.DeserializeObject<UserEntity>(apiStringResponse);
                return user;
            }
            else
            {
                return null;
            }
        }
    }
}
