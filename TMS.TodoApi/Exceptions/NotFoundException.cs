using System.Net.Http;

namespace TMS.TodoApi.Exceptions
{
    public class NotFoundException : HttpResponseException
    {
        public NotFoundException(HttpResponseMessage response) : base(response)
        {
        }
    }
}
