
namespace EgitimPortali.Application.Response
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public BaseResponse(bool pSuccess, string pMessage)
        {
            Success = pSuccess;
            Message = pMessage;
        }

    }
}
