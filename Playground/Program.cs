using System.Collections;
using System.Net.Http.Json;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;


var res = await FetchSkillsFromLinkedin("vladislavg123");
Console.WriteLine(res);


async Task<RockDevsLinkedInApi> FetchSkillsFromLinkedin(string username)
{
    using var client = new HttpClient();
    using var response = await client.SendAsync(new HttpRequestMessage
    {
        Method = HttpMethod.Get,
        RequestUri = new Uri($"https://linkedin-profile-data-api.p.rapidapi.com/?username={username}"),
        Headers =
        {
            { "X-RapidAPI-Key", "384d62cd25mshe08e0874f7810d8p18766ajsn8a22f40bc584" },
            { "X-RapidAPI-Host", "linkedin-profile-data-api.p.rapidapi.com" },
        },
    });
    response.EnsureSuccessStatusCode();

    var result = await response.Content.ReadFromJsonAsync<RockDevsLinkedInApi>();

    return result!;
}


record RockDevsLinkedInApi(List<LinkedInSkillResponse> Skills);

record LinkedInSkillResponse(string Name);