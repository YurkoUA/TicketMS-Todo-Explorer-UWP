using System;
using Newtonsoft.Json;

namespace TMS.TodoApi.Models
{
    public class Token
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public int SecondsToExpire { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        public DateTime CreationTime { get; set; } = DateTime.Now;
        public DateTime ExpireTime => CreationTime.AddSeconds(SecondsToExpire);

        public bool IsActive => DateTime.Now < ExpireTime;

        #region System.Object methods

        public override string ToString()
        {
            return AccessToken;
        }

        #endregion
    }
}
