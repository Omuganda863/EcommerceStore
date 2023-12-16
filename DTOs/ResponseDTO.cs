using System.Net;

namespace EcommerceStore.DTOs
{
    public class ResponseDTO
    {
        public HttpStatusCode StatusCode { get; set; }=HttpStatusCode.OK;
        public string Message { get; set; } = string.Empty;
        public object Result { get; set; } = default!;
    }
}
