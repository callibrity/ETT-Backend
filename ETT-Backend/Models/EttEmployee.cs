using Newtonsoft.Json;
using System;

namespace ETT_Backend.Models
{
  public class EttEmployee
  {
    [JsonProperty("first_name")]
    public string FirstName { get; set; }

    [JsonProperty("last_name")]
    public string LastName { get; set; }

    [JsonProperty("role")]
    public string Role { get; set; }

    public EttEmployee(string firstName, string lastName, string empRole)
    {
      this.FirstName = firstName;
      this.LastName = lastName;
      this.Role = empRole;
    }
  }
}
