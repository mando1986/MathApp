using System.Net.Http;
using System.Net.Http.Json;

namespace MathApp.Services;

public class MathAppService
{
    HttpClient httpClient;

    public MathAppService()
    {
        this.httpClient = new HttpClient();
    }

    List<Computation> computationList;

    public async Task<List<Computation>> GetComputation()
    {
        if (computationList?.Count > 0)
            return computationList;

        // http uri
        var response = await httpClient.GetAsync("https://ttpham13.github.io/MathApi/MathAppdB.json");
        if (response.IsSuccessStatusCode)
        {
            computationList = await response.Content.ReadFromJsonAsync(ComputationContext.Default.ListComputation);
        }

        // api uri
        //var apiResponse = await httpClient.GetAsync("http://localhost:5067/generate-exercise");
        //if (apiResponse.IsSuccessStatusCode)
        //{
        //    computationList = await apiResponse.Content.ReadFromJsonAsync<List<Computation>>();
        //}


        return computationList;
    }
}