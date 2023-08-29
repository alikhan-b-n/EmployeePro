using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace EmployeePro.Bll.Dtos;

public class TotalInfoFromApis
{
    public string ProfilePicUrl { get; set; }
    
    public string FullName { get; set; }
    
    public string Summary { get; set; }
    
    public List<string> Languages { get; set; }
    
    public List<Education> Education { get; set; }
    
    public List<Experience> Experiences { get; set; }

    public RockDevsLinkedInApiDto Skills { get; set; }
}
public class ApisWorldLinkedinApiDto
{
    [JsonProperty("public_identifier")]
    [JsonPropertyName("public_identifier")]
    public string PublicIdentifier { get; set; }

    [JsonProperty("profile_pic_url")]
    [JsonPropertyName("profile_pic_url")]
    public string ProfilePicUrl { get; set; }

    [JsonProperty("full_name")]
    [JsonPropertyName("full_name")]
    public string FullName { get; set; }

    [JsonProperty("summary")]
    [JsonPropertyName("summary")]
    public string Summary { get; set; }

    [JsonProperty("languages")]
    [JsonPropertyName("languages")]
    public List<string> Languages { get; set; }

    [JsonProperty("education")]
    [JsonPropertyName("education")]
    public List<Education> Education { get; set; }

    [JsonProperty("experiences")]
    [JsonPropertyName("experiences")]
    public List<Experience> Experiences { get; set; }
}

public class Education
{
    [JsonProperty("starts_at")]
    [JsonPropertyName("starts_at")]
    public WhenAt? StartAt { get; set; }

    [JsonProperty("ends_at")]
    [JsonPropertyName("ends_at")]
    public WhenAt? EndsAt { get; set; }

    [JsonProperty("field_of_study")]
    [JsonPropertyName("field_of_study")]
    public object FieldOfStudy { get; set; }

    [JsonProperty("degree_name")]
    [JsonPropertyName("degree_name")]
    public object DegreeName { get; set; }

    [JsonProperty("school")]
    [JsonPropertyName("school")]
    public string School { get; set; }

    [JsonProperty("description")]
    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonProperty("logo_url")]
    [JsonPropertyName("logo_url")]
    public string LogoUrl { get; set; }
}

public class Experience
{
    [JsonProperty("starts_at")]
    [JsonPropertyName("starts_at")]
    public WhenAt? StartAt { get; set; }

    [JsonProperty("ends_at")]
    [JsonPropertyName("ends_at")]
    public WhenAt? EndsAt { get; set; }

    [JsonProperty("company")]
    [JsonPropertyName("company")]
    public string Company { get; set; }

    [JsonProperty("title")]
    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonProperty("description")]
    [JsonPropertyName("description")]
    public string Description { get; set; }
}

public class WhenAt
{
    [JsonProperty("day")]
    [JsonPropertyName("day")]
    public int? Day { get; set; }

    [JsonProperty("month")]
    [JsonPropertyName("month")]
    public int? Month { get; set; }

    [JsonProperty("year")]
    [JsonPropertyName("year")]
    public int? Year { get; set; }
}

public class RockDevsLinkedInApiDto
{
    public List<LinkedInSkill> Skills { get; set; }
}

public class LinkedInSkill
{
    public string Name { get; set; }
}