using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TMS.TodoApi.Exceptions
{
    public class BadRequestException : HttpResponseException
    {
        public BadRequestException(HttpResponseMessage response) : base(response)
        {
        }

        public async Task<IEnumerable<string>> GetErrorsAsync()
        {
            var content = await Response.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(content))
                return null;

            try
            {
                return JsonConvert.DeserializeObject<IEnumerable<string>>(content);
            }
            catch
            {
                return null;
            }
        }
    }
}
