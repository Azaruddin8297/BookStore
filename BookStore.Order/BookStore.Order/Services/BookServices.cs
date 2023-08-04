using BookStore.Order.Entity;
using BookStore.Order.Interface;
using Newtonsoft.Json;

namespace BookStore.Order.Services
{
    public class BookServices : IBookServices
    {
        private readonly IConfiguration _config;
        public BookServices(IConfiguration config)
        {
            _config = config;
        }
        public async Task<BookEntity> GetBookById(int id)
        {
            //BookEntity book = null;
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"https://localhost:7156/api/book/GetBookById?id={id}");

                if (response.IsSuccessStatusCode)
                {
                    //apicontent -> string ->convert it as ResponseEntity -> as string -> converted to BookEntity
                    string apiContent = await response.Content.ReadAsStringAsync();
                    ResponseEntity responseEntity = JsonConvert.DeserializeObject<ResponseEntity>(apiContent);
                    string bookContent = responseEntity.Data.ToString();
                    BookEntity book = JsonConvert.DeserializeObject<BookEntity>(bookContent);

                    return book;
                }
                return null;
            }
        }
    }
}
