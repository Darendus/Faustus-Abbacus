using System.Net.Http;
using System.Threading.Tasks;

namespace FaustsAbbacus.Services
{
    public class PoeNinjaService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://poe.ninja/api/data";

        public PoeNinjaService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<decimal> GetItemPrice(string itemName, string league)
        {
            // TODO: Implementiere den konkreten API-Call zu poe.ninja
            // Die genaue Implementierung hängt von der poe.ninja API-Struktur ab
            return 0m;
        }
    }
}