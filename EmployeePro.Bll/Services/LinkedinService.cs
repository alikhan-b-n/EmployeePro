using System.Collections;
using EmployeePro.Bll.Dtos;

namespace EmployeePro.Bll.Services;
/// <summary>
/// Service for fetching LinkedIn profile data.
/// </summary>
public class LinkedinService : ILinkedinService
{
    private readonly IHttpClientFactory _factory;

    public LinkedinService(IHttpClientFactory factory)
    {
        _factory = factory;
    }
    
    /// <summary>
    /// Sums up two methods bellow. Returns total information about user from his linkedin profile
    /// </summary>
    /// <param name="url">Url of linkedin profile</param>
    /// <returns>TotalInfoFromApis</returns>
    public async Task<TotalInfoFromApis> GetInfoFromLinkedinUrl(string url)
    {
        var profileData = await FetchDataFromLinkedin(url);
        var skills = await FetchSkillsFromLinkedin(profileData.PublicIdentifier);

        return new TotalInfoFromApis
        {
            FullName = profileData.FullName,
            ProfilePicUrl = profileData.ProfilePicUrl,
            Summary = profileData.Summary,
            Languages = profileData.Languages,
            Skills = skills ?? null,
            Experiences = profileData.Experiences,
            Education = profileData.Education
        };
    }
    /// <summary>
    /// Gets all info of employee from linkedin url besides skills from ApisWorld Api and fetching it into ApisWorldLinkedinApiDto
    /// </summary>
    /// <param name="url">Url of linkedin profile</param>
    /// <returns>ApisWorldLinkedinApiDto</returns>
    public async Task<ApisWorldLinkedinApiDto?> FetchDataFromLinkedin(string url)
    {
        using var client = _factory.CreateClient("FetchDataFromLinkedin");
        using var response = await client.SendAsync(new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://linkedin-profile-data.p.rapidapi.com/linkedin-data?url={url}"),
            Headers =
            {
                { "X-RapidAPI-Key", "384d62cd25mshe08e0874f7810d8p18766ajsn8a22f40bc584" },
                { "X-RapidAPI-Host", "linkedin-profile-data.p.rapidapi.com" },
            },
        });
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ApisWorldLinkedinApiDto>();
    }
    
    /// <summary>
    /// Gets skills from linkedin url from RockDevs Api
    /// </summary>
    /// <param name="username"></param>
    /// <returns>RockDevsLinkedInApiDto</returns>
    public async Task<RockDevsLinkedInApiDto?> FetchSkillsFromLinkedin(string username)
    {
        using var client = _factory.CreateClient("FetchSkillsFromLinkedin");
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


        return await response.Content.ReadFromJsonAsync<RockDevsLinkedInApiDto>();
    }
}