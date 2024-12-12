namespace ProductService.Services
{
    public class CategoryClientService
    {
        private readonly HttpClient _httpClient;

        public CategoryClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Category> GetCategoryAsync(int IdCategory)
        {
            var response = await _httpClient.GetAsync($"/api/category/{IdCategory}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Category>();
        }
    }

    public class Category 
    { 
        public int Id { get; set; } 
        public string Name { get; set; } 
    }
}
