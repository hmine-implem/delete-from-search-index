using System.Text;
using Newtonsoft.Json;

static class Program
{
    private const string InputFile = "input.json";
    private const string AiSearchUrl = "https://{Azure AI Search Service Name}.search.windows.net/indexes('{Index Name}')/docs/search.index?api-version=2024-07-01";
    private const string AiSearchKey = "{Azure AI Search API Key}";
    private static readonly HttpClient httpClient = new()
    {
        BaseAddress = new Uri(AiSearchUrl),
        DefaultRequestHeaders = { { "api-key", AiSearchKey } },
    };    
    static async Task Main()
    {
        try {
            await PostAsync(httpClient);
        } catch (Exception ex) {
            Console.WriteLine(ex.Message);
        } finally {
            httpClient.Dispose();
            Console.WriteLine("Complete: Delete from search index.");
        }
    }
    static async Task PostAsync(HttpClient httpClient)
    {
        using StringContent jsonContent = new(
            content: JsonConvert.SerializeObject(new RequestContent {
                Values = GetRequestValues() }),
            encoding: Encoding.UTF8,
            mediaType: "application/json");
        using HttpResponseMessage response = await httpClient.PostAsync(
            requestUri: httpClient.BaseAddress,
            content: jsonContent);
        var jsonResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"{jsonResponse}\n");
    }
    private static List<Value> GetRequestValues(){
        var result = new List<Value>();
        using (var reader = new StreamReader(InputFile, Encoding.UTF8)){
            var input = default(CurrentIndexContent);
            try{
                input = JsonConvert.DeserializeObject<CurrentIndexContent>(reader.ReadToEnd());
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            if (input == null) { return result; }
            foreach (var item in input.Values){
                result.Add(new Value { SearchAction = "delete", Id = item.Id });
            }
        }
        return result;
    }
}

class RequestContent {
    [JsonProperty("value")]
    public required List<Value> Values { get; set; }
}

class Value {
    [JsonProperty("@search.action")]
    public required string SearchAction { get; set; }
    [JsonProperty("ID")]
    public required string Id { get; set; }
}

class CurrentIndexContent {
    [JsonProperty("@odata.context")]
    public required string OdataContext { get; set; }
    [JsonProperty("@odata.count")]
    public required int OdataCount { get; set; }
    [JsonProperty("value")]
    public required List<CurrentIndexValue> Values { get; set; }
}

class CurrentIndexValue {
    [JsonProperty("@search.score")]
    public required string SearchScore { get; set; }
    [JsonProperty("ID")]
    public required string Id { get; set; }
    [JsonProperty("StoreName")]
    public required string StoreName { get; set; }
    [JsonProperty("Reviews")]
    public required string Reviews { get; set; }
    [JsonProperty("Location")]
    public required string Location { get; set; }
    [JsonProperty("Style")]
    public required string Style { get; set; }
    [JsonProperty("RecommendedMenu")]
    public required string RecommendedMenu { get; set; }
    [JsonProperty("Keyword")]
    public required string Keyword { get; set; }
}
