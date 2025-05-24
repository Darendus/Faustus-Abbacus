using System.Net.Http;
using System.Text.Json;
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

        public async Task<decimal> GetItemPrice(string itemName, string league, string itemType)
        {
            string endpoint = itemType switch
            {
                "Currency" => $"{BaseUrl}/currencyoverview",
                "Fragment" => $"{BaseUrl}/currencyoverview",
                _ => $"{BaseUrl}/itemoverview"
            };

            string type = itemType switch
            {
                "Currency" => "Currency",
                "Fragment" => "Fragment",
                _ => itemType
            };

            var url = $"{endpoint}?league={league}&type={type}";
            
            try
            {
                var response = await _httpClient.GetStringAsync(url);
                var jsonDocument = JsonDocument.Parse(response);
                var lines = jsonDocument.RootElement.GetProperty("lines");

                foreach (var line in lines.EnumerateArray())
                {
                    var currName = line.GetProperty("currencyTypeName").GetString();
                    if (itemType == "Currency" || itemType == "Fragment")
                    {
                        if (currName == itemName)
                        {
                            return line.GetProperty("chaosEquivalent").GetDecimal();
                        }
                    }
                    else
                    {
                        var name = line.GetProperty("name").GetString();
                        if (name == itemName)
                        {
                            return line.GetProperty("chaosValue").GetDecimal();
                        }
                    }
                }
                return 0m;
            }
            catch
            {
                return 0m;
            }
        }
    }
}